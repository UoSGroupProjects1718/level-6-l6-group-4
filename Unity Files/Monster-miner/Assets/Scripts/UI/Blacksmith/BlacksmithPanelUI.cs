using UnityEngine.UI;
using UnityEngine;

public class BlacksmithPanelUI : MonoBehaviour {

    [SerializeField]
    private GameObject recipeButton;
    private Transform contentParent;

    public void Start()
    {
        contentParent = UIPanels.Instance.blacksmithPanel.transform.Find("Blacksmith main panel").Find("Scroll View").Find("Viewport").Find("RecipeContent").transform;
        for(int i = 0; i < UIPanels.Instance.blacksmithCraftingRecipes.Length; i++)
        {
            GameObject button = Instantiate(recipeButton, contentParent);
            button.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = UIPanels.Instance.blacksmithCraftingRecipes[i].craftedItem.uiSprite;
            button.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = UIPanels.Instance.blacksmithCraftingRecipes[i].craftedItem.itemName;
        }
    }

    public void OnPanelOpen()
    {
        
    }
}
