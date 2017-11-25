using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlacksmithPanelUI : MonoBehaviour {

    [SerializeField]
    private GameObject recipeButton;
    private Transform contentParent;

    private Transform tertiaryPanel;
    private Craftable focusedRecipe;
    [SerializeField]
    private Sprite requiredResourcesBaseSprite;

    public void Start()
    {
        //get the content parent of the scroll view
        contentParent = UIPanels.Instance.blacksmithPanel.transform.Find("Blacksmith main panel").Find("Scroll View").Find("Viewport").Find("RecipeContent").transform;
        //get the tertiary panel
        tertiaryPanel = UIPanels.Instance.blacksmithPanel.transform.GetChild(1);
        //loop through all of the crafting recipes
        for (int i = 0; i < UIPanels.Instance.blacksmithCraftingRecipes.Length; i++)
        {
            //and make a new button, then set the image and text of the button and add a listener.
            GameObject button = Instantiate(recipeButton, contentParent);
            button.transform.localScale = Vector3.one;
            button.GetComponent<Button>().onClick.AddListener(OnRecipeButtonPress);
            button.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = UIPanels.Instance.blacksmithCraftingRecipes[i].craftedItem.uiSprite;
            button.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = UIPanels.Instance.blacksmithCraftingRecipes[i].craftedItem.itemName;
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
                requiredResources.GetChild(i).GetChild(0).GetComponent<Image>().sprite = requiredResourcesBaseSprite;
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

            //set the focused recipe so we know what to do when we click on the button again
            focusedRecipe = correspondingRecipe;
            //set the sprite and text of the corresponding recipe
            tertiaryPanel.GetChild(0).GetComponent<Image>().sprite = correspondingRecipe.craftedItem.uiSprite;
            tertiaryPanel.GetChild(1).GetComponent<Text>().text = correspondingRecipe.craftedItem.itemName;
            //loop through the images for the required items
            for(int j = 0; j < correspondingRecipe.requiredItems.Length; j++ )
            {
                //and set the sprite and number required
                requiredResources.GetChild(j).GetChild(0).GetComponent<Image>().sprite = UIPanels.Instance.GetResourceSprite(correspondingRecipe.requiredItems[j].resource);
                requiredResources.GetChild(j).GetChild(1).GetComponent<Text>().text = correspondingRecipe.requiredItems[j].requiredAmount.ToString();
            }

            //activate the tertiary panel
            tertiaryPanel.gameObject.SetActive(true);
        }

        
    }
}
