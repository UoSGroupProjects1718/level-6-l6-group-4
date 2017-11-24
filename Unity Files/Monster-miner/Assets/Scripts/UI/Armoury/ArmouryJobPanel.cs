using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ArmouryJobPanel : MonoBehaviour
{

    public Transform colonistContentParent;
    public Transform wearableContentParent;

    [SerializeField]
    private GameObject colonistButton;
    [SerializeField]
    private GameObject wearableButton;

    private Transform colonistPicPoolParent;//parent transform of the colonist pictures used in the main panel
    private Transform wearablePicPoolParent;//parent transform of the wearable pictures used in the tertiary panel

    private List<GameObject> colonistPicturePool;
    private List<GameObject> wearablePicturePool;

    private ColonistController focusedColonist;
    string[] wearableSlugs;

   [SerializeField]
    private int poolSize = 40;


    void Awake()
    {

        //initialise the parent transforms for the object pools
        colonistPicPoolParent = new GameObject().transform;
        colonistPicPoolParent.name = "ColonistPicturePool";

        wearablePicPoolParent = new GameObject().transform;
        wearablePicPoolParent.name = "WearablePicturePool";


        colonistPicturePool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject panel = Instantiate(colonistButton, colonistPicPoolParent) as GameObject;
            colonistPicturePool.Add(panel);
            panel.GetComponentInChildren<Button>().onClick.AddListener(ColonistButtonClick);
            panel.SetActive(false);
        }
        wearablePicturePool = new List<GameObject>();
        for (int i = 0; i < ItemDatabase.NumWearables; i++)
        {
            GameObject panel = Instantiate(wearableButton, wearablePicPoolParent) as GameObject;
            wearablePicturePool.Add(panel);
            panel.GetComponentInChildren<Button>().onClick.AddListener(WearableButtonClick);
            panel.SetActive(false);

        }



    }
    private GameObject GetColonistPicture()
    {
        for (int i = 0; i < colonistPicturePool.Count; i++)
        {
            if (!colonistPicturePool[i].activeSelf)
            {
                colonistPicturePool[i].SetActive(true);
                colonistPicturePool[i].transform.SetParent(colonistContentParent);
                return colonistPicturePool[i];
            }
        }
        return null;
    }

    private GameObject GetWearablePicture()
    {
        for (int i = 0; i < wearablePicturePool.Count; i++)
        {
            if (!wearablePicturePool[i].activeSelf)
            {
                wearablePicturePool[i].SetActive(true);
                wearablePicturePool[i].transform.SetParent(wearableContentParent);
                return wearablePicturePool[i];
            }
        }
        return null;
    }

    public void OnPanelOpen()
    {
        for (int i = 0; i < BehaviourTreeManager.Colonists.Count; i++)
        {
            GameObject picture = GetColonistPicture();
            if (picture != null)
            {
                switch (BehaviourTreeManager.Colonists[i].colonistJob)
                {
                    case ColonistJobType.Builder:
                        picture.transform.GetChild(0).GetComponent<Image>().color = Color.blue;
                        break;
                    case ColonistJobType.Hunter:
                        picture.transform.GetChild(0).GetComponent<Image>().color = Color.red;
                        break;
                    case ColonistJobType.Scout:
                        picture.transform.GetChild(0).GetComponent<Image>().color = Color.green;
                        break;
                    default:
                        Debug.Log("Colonist is not of valid job type");
                        break;
                }
            }
        }
        wearableSlugs = Stockpile.Instance.WearableSlugs;
        for (int j = 0; j < Stockpile.Instance.WearableSlugs.Length; j++)
        {
            GameObject picture = GetWearablePicture();
            
            if (picture != null)
            {
                //set the image
                picture.transform.GetChild(0).GetComponent<Image>().sprite = ItemDatabase.GetItem(wearableSlugs[j]).uiSprite;
                //set the quantity available
                picture.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Stockpile.Instance.wearableInventoryDictionary[ItemDatabase.GetItem(wearableSlugs[j]) as Wearable].ToString();
            }
        }

    }
    private void OnTertiaryPanelOpen()
    {
        wearableSlugs = Stockpile.Instance.WearableSlugs;
        for (int j = 0; j < Stockpile.Instance.WearableSlugs.Length; j++)
        {
            GameObject picture = GetWearablePicture();

            if (picture != null)
            {
                //set the image
                picture.transform.GetChild(0).GetComponent<Image>().sprite = ItemDatabase.GetItem(wearableSlugs[j]).uiSprite;
                //set the quantity available
                picture.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Stockpile.Instance.wearableInventoryDictionary[ItemDatabase.GetItem(wearableSlugs[j]) as Wearable].ToString();
            }
        }
    }
    //on disable reset the pictures
    public void OnDisable()
    {
        while (colonistContentParent.transform.childCount > 0)
        {

            if (colonistPicPoolParent != null)
            {
                colonistContentParent.transform.GetChild(0).gameObject.SetActive(false);
                colonistContentParent.transform.GetChild(0).SetParent(colonistPicPoolParent.transform);
            }
            else
            {
                break;
            }
        }
        while (wearableContentParent.transform.childCount > 0)
        {

            if (wearablePicPoolParent != null)
            {
                wearableContentParent.transform.GetChild(0).gameObject.SetActive(false);
                wearableContentParent.transform.GetChild(0).SetParent(wearablePicPoolParent.transform);
            }
            else
            {
                break;
            }
        }
    }

    public void ColonistButtonClick()
    {
        //get a reference to the teriary panel to keep the code a little cleaner
        GameObject tertiaryPanel = UIPanels.Instance.armouryPanel.transform.GetChild(1).gameObject;
            //get the correct colonist from the list based upon the index of the child transform
            int colonistIndex = EventSystem.current.currentSelectedGameObject.transform.parent.GetSiblingIndex();
            ColonistController colonist = BehaviourTreeManager.Colonists[colonistIndex];


        //then check to see if it is active
        if(tertiaryPanel.activeSelf && focusedColonist == colonist )
        {
            //if it is, we simply need to close it
            tertiaryPanel.SetActive(false);
        }
        //otherwise if it isnt active
        else
        {


            focusedColonist = colonist;
            //and set the name text
            tertiaryPanel.transform.GetChild(0).GetComponent<Text>().text =colonist.colonistName;

            //then we want to set the equipped item information and the colonist picture
            Transform equippedItemImages = tertiaryPanel.transform.GetChild(1);
            //set the colour of the image (this will be replaced with sprites for each job type)
            switch (colonist.colonistJob)
            {
                case ColonistJobType.Builder:
                    equippedItemImages.GetChild(0).GetComponent<Image>().color = Color.blue;
                    break;
                case ColonistJobType.Hunter:
                    equippedItemImages.GetChild(0).GetComponent<Image>().color = Color.red;
                    break;
                case ColonistJobType.Scout:
                    equippedItemImages.GetChild(0).GetComponent<Image>().color = Color.green;
                    break;
                default:
                    Debug.Log("Colonist is not of valid job type");
                    break;
            }
            //then get each of the images and apply the item's ui sprite to them.
            equippedItemImages.GetChild(1).GetComponent<Image>().sprite = colonist.colonistEquipment.equippedArmour[0].uiSprite;
            equippedItemImages.GetChild(2).GetComponent<Image>().sprite = colonist.colonistEquipment.equippedArmour[1].uiSprite;
            equippedItemImages.GetChild(3).GetComponent<Image>().sprite = colonist.colonistEquipment.equippedArmour[2].uiSprite;
            equippedItemImages.GetChild(4).GetComponent<Image>().sprite = colonist.colonistEquipment.weapon.uiSprite;
            //then activate the panel
            tertiaryPanel.SetActive(true);
        }
    }

    public void WearableButtonClick()
    {
        //get the item
        int buttonIndex = EventSystem.current.currentSelectedGameObject.transform.parent.GetSiblingIndex();
        Wearable correspondingWearable = ItemDatabase.GetItem(wearableSlugs[buttonIndex]) as Wearable;
        //first we need to find what item will be unequipped
        Wearable unequippedItem = focusedColonist.colonistEquipment.equippedArmour[(int)correspondingWearable.armourSlot];
        //if the two items we are trying to swap are the same, just return, we dont need to do anything more
        if (correspondingWearable == unequippedItem)
            return;

        if (Stockpile.Instance.removeWearable(correspondingWearable))
        {
            //and if the stockpile does not already contain the unequipped item, we need to create a new button for it.
            if (!Stockpile.Instance.wearableInventoryDictionary.ContainsKey(unequippedItem))
            {
                GameObject newButton = GetWearablePicture();
            }
            //then add the item to the stockpile
            Stockpile.Instance.AddWearable(focusedColonist.colonistEquipment.equippedArmour[(int)correspondingWearable.armourSlot]);
            //then unequip the item in the item slot's spot
            focusedColonist.colonistEquipment.UnequipArmour(correspondingWearable.armourSlot);

            //now we need to equip the new item
            focusedColonist.colonistEquipment.EquipWearable(correspondingWearable);
            //and set the equipped item image
            UIPanels.Instance.armouryPanel.transform.GetChild(1).GetChild(1).GetChild((int)correspondingWearable.armourSlot + 1).GetComponent<Image>().sprite = correspondingWearable.uiSprite;
            //and remove the corresponding button and get a new one 
            if (!Stockpile.Instance.wearableInventoryDictionary.ContainsKey(correspondingWearable))
            {
                //so if the wearable was no longer in the dictionary, i.e. it's stack amount became 0, we get rid of the button
                wearableContentParent.GetChild(buttonIndex).gameObject.SetActive(false);
                wearableContentParent.GetChild(buttonIndex).SetParent(wearablePicPoolParent);
            }
            
            //now we need to re-distribute the ui sprites, because we may have gained or lost a button
            wearableSlugs = Stockpile.Instance.WearableSlugs;
            for(int i = 0; i < wearableSlugs.Length; i++)
            {
                //set the image of the button
                wearableContentParent.GetChild(i).GetChild(0).GetComponent<Image>().sprite = ItemDatabase.GetItem(wearableSlugs[i]).uiSprite;
                //set the quantity of the item available
                wearableContentParent.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>().text = Stockpile.Instance.wearableInventoryDictionary[ItemDatabase.GetItem(wearableSlugs[i]) as Wearable].ToString();
            }
        }
    }
}
