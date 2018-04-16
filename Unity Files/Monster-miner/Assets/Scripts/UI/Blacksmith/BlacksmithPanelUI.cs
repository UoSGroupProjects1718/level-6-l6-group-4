using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BlacksmithPanelUI : MonoBehaviour {

    [SerializeField]
    private GameObject recipeButton;
    private Transform contentParent;

    private Transform tertiaryPanel;
    private Craftable focusedRecipe;


    public void Start()
    {
        //get the content parent of the scroll view
        contentParent = UIPanels.Instance.blacksmithPanel.transform.Find("Blacksmith main panel").Find("Scroll View").Find("Viewport").Find("RecipeContent").transform;
        //get the tertiary panel
        tertiaryPanel = UIPanels.Instance.blacksmithPanel.transform.GetChild(1);
        //loop through all of the crafting recipes
        for (int i = 0; i < UIPanels.Instance.blacksmithCraftingRecipes.Length; i++)
        {
            //and make a new button and add a listener.
            GameObject button = Instantiate(recipeButton, contentParent);
            button.transform.localScale = Vector3.one; //have to reset the scale to one otherwise unity automatically re-scales the object
            button.GetComponent<Button>().onClick.AddListener(delegate { OnRecipeButtonPress(); });
            //then set the image
            button.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = UIPanels.Instance.blacksmithCraftingRecipes[i].craftedItem.uiSprite;
            //and text of the button
            button.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = UIPanels.Instance.blacksmithCraftingRecipes[i].craftedItem.itemName;

            if((UIPanels.Instance.blacksmithCraftingRecipes[i].craftedItem as Wearable).armourSlot == ArmourSlot.Weapon)
            {
                button.AddComponent<WeaponTooltip>();
            }
            else
            {
                button.AddComponent<ArmourTooltip>();
            }

            //then let the tooltip class know which item to reference
            button.GetComponent<ItemTooltip>().referenceItem = UIPanels.Instance.blacksmithCraftingRecipes[i].craftedItem;
        }
    }
    public void OnRecipeButtonPress()
    {
        //get the transform of the clicked button
        Transform pressedButton = EventSystem.current.currentSelectedGameObject.transform;
        //and get the recipe that the button corresponds to
        Craftable correspondingRecipe = UIPanels.Instance.blacksmithCraftingRecipes[pressedButton.GetSiblingIndex()];

        //get the parent for the required resources
         Transform requiredResources = tertiaryPanel.transform.GetChild(4);


            for(int i = 0; i < requiredResources.childCount; i++)
            {
                requiredResources.GetChild(i).GetChild(0).GetComponent<Image>().sprite = null;
                requiredResources.GetChild(i).GetComponentInChildren<Text>().text = string.Empty;
            }
        //if the tertiary panel is open and the focused recipe is the current one (we are pressing the same button again)
        if(tertiaryPanel.gameObject.activeSelf && focusedRecipe == correspondingRecipe)
        {
            //deactivate it
            tertiaryPanel.gameObject.SetActive(false);
            UIPanels.Instance.blacksmithPanel.transform.GetChild(2).gameObject.SetActive(false);
        }
        //otherwise we need to open it
        else
        {

            UIPanels.Instance.blacksmithInputField.text = "1";
            //set the focused recipe so we know what to do when we click on the button again
            focusedRecipe = correspondingRecipe;
            //set the sprite and text of the corresponding recipe
            tertiaryPanel.GetChild(0).GetComponent<Image>().sprite = correspondingRecipe.craftedItem.uiSprite;
            tertiaryPanel.GetChild(1).GetComponent<TextMeshProUGUI>().text = correspondingRecipe.craftedItem.itemName;
            //loop through the images for the required items
            for(int j = 0; j < 3; j++ )
            {
                if(j < correspondingRecipe.requiredItems.Length)
                {
                    //and set the sprite and number required
                    requiredResources.GetChild(j).GetChild(0).GetComponent<Image>().sprite = UIPanels.Instance.GetResourceSprite(correspondingRecipe.requiredItems[j].resource);
                    requiredResources.GetChild(j).GetChild(0).GetComponent<Image>().color = Color.white;
                    requiredResources.GetChild(j).GetChild(1).GetComponent<Text>().text = correspondingRecipe.requiredItems[j].requiredAmount.ToString();
                }
                else
                {
                    requiredResources.GetChild(j).GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
                }
            }
     
            //activate the tertiary panel
            tertiaryPanel.gameObject.SetActive(true);
        }

        
    }
    public void OnConfirmButtonPress()
    {
        for(int i = 0; i < int.Parse(UIPanels.Instance.blacksmithInputField.text); i++)
        {
            RequiredItem[] requiredItems = new RequiredItem[focusedRecipe.requiredItems.Length];
            for(int j  =0; j < requiredItems.Length; j++)
            {
                requiredItems[j].requiredAmount = focusedRecipe.requiredItems[j].requiredAmount;
                requiredItems[j].resource = focusedRecipe.requiredItems[j].resource;
            }

            JobManager.CreateJob(JobType.Crafting, requiredItems, focusedRecipe.workAmount, focusedRecipe.craftedItem, Vector3.zero, "Create " + focusedRecipe.craftedItem.itemName);
        }
    }

    public void IncrementBlacksmithInputField()
    {
        int number = int.Parse(UIPanels.Instance.blacksmithInputField.text);
        number++;
        UIPanels.Instance.blacksmithInputField.text = number.ToString();
    }
    public void DecrementBlacksmithInputField()
    {
        int number = int.Parse(UIPanels.Instance.blacksmithInputField.text);
        if (number > 1)
        {
            number--;
            UIPanels.Instance.blacksmithInputField.text = number.ToString();
        }
    }
}
