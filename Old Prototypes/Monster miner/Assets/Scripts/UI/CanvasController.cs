using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
public enum UIType
{
    Inventory,
    Crafting,
}

public class CanvasController : MonoBehaviour {

    public GameObject buildingScroll;
    public GameObject toggleGrid;

    public GameObject UITarget;

    public GameObject InventoryElement;
    public GameObject CraftingElement;

    public GameObject currentUIElement;



    #region stockpile debugging
    public RawMaterial DebugStone;
    public RawMaterial DebugWood;
#endregion

    #region singleton
    private static CanvasController Instance;
    public static CanvasController GetInstance()
    {
        return Instance;
    }
#endregion

    private void Awake()
    {
        Instance = this;
        toggleGrid = GameObject.Find("DrawGrid");
        //find the ui elements, then set them to inactive
        InventoryElement = GameObject.Find("InventoryElement");
        CraftingElement = GameObject.Find("CraftingElement");
        InventoryElement.SetActive(false);
        CraftingElement.SetActive(false);
    }

    public void OnBuildingsClick()
    {
        buildingScroll.SetActive(!buildingScroll.activeSelf);
    }
    public void OnToggleGridClick()
    {
        toggleGrid.SetActive(!toggleGrid.activeSelf);
    }
    public void OnCloseButtonClick()
    {
        currentUIElement.SetActive(false);
    }

    public void DisplayUI(UIType uiType,PlaceableBuilding building)
    {
        switch (uiType)
        {
            case UIType.Inventory:
                currentUIElement = InventoryElement;
                UpdateUI();
                currentUIElement.SetActive(true);
                break;
            case UIType.Crafting:
                currentUIElement = InventoryElement;
                UpdateUI();
                currentUIElement.SetActive(true);
                break;
            default:
                break;
        }
    }


    #region stockpiledebugfunction
    public void DebugAddResource()
    {
        Dropdown dropdown = currentUIElement.transform.GetChild(2).GetComponent<Dropdown>();

        Dropdown numberDropdown = currentUIElement.transform.GetChild(4).GetComponent<Dropdown>();

        int numberOfTimes = int.Parse(numberDropdown.captionText.text);

        switch (dropdown.captionText.text)
        {
            case "Wood":
                RawMaterial wood = DebugWood;
                if(numberOfTimes > 1)
                {
                UITarget.GetComponent<StockpileFunction>().AddItem(wood, numberOfTimes);
                }
                else
                {
                    UITarget.GetComponent<StockpileFunction>().AddItem(wood);

                }
                break;
            case "Stone":
                RawMaterial stone = DebugStone;
                if (numberOfTimes > 1)
                {
                    UITarget.GetComponent<StockpileFunction>().AddItem(stone, numberOfTimes);
                }
                else
                {
                    UITarget.GetComponent<StockpileFunction>().AddItem(stone);

                }
                break;
            default:
                break;
        }
    }
    public void DebugRemoveResource()
    {
        Dropdown dropdown = currentUIElement.transform.GetChild(3).GetComponent<Dropdown>();

        Dropdown numberDropdown = currentUIElement.transform.GetChild(4).GetComponent<Dropdown>();

        int numberOfTimes = int.Parse(numberDropdown.captionText.text);

        switch (dropdown.captionText.text)
        {
            case "Wood":
               if(numberOfTimes > 1)
                    UITarget.GetComponent<StockpileFunction>().RemoveItem(RawMaterialType.Wood,numberOfTimes);
               else
                    UITarget.GetComponent<StockpileFunction>().RemoveItem(RawMaterialType.Wood);
                break;
            case "Stone":
                if(numberOfTimes > 1)
                    UITarget.GetComponent<StockpileFunction>().RemoveItem(RawMaterialType.Stone, numberOfTimes);
                else
                    UITarget.GetComponent<StockpileFunction>().RemoveItem(RawMaterialType.Stone);
                break;
            default:
                break;
        }
    }
    #endregion

    public void UpdateUI()
    {
        if(currentUIElement == InventoryElement)
        {
            Transform Inventory = currentUIElement.transform.Find("Inventory").Find("Background");
            InventoryBuilding building = UITarget.GetComponent<InventoryBuilding>();
            for (int i = 0; i < building.inventory.Length; i++)
            {
                Transform child = Inventory.GetChild(i);
                if(building.inventory[i] != null)
                {
                    child.GetChild(0).GetComponent<Image>().sprite = building.inventory[i].displaySprite;
                    child.GetChild(1).GetComponent<Text>().text = building.inventory[i].Name;
                    child.GetChild(2).GetComponent<Text>().text = building.inventory[i].currentStackSize.ToString();
                    child.gameObject.SetActive(true);
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        else if(currentUIElement == CraftingElement)
        {
            return;
        }
    }


    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D col = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.4f);
            if (currentUIElement != null && col == null)
            {
                UITarget = null;
                currentUIElement.SetActive(false);
                currentUIElement = null;
            }
        }
    }
}
