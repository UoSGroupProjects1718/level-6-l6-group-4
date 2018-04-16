using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BuildingTooltip : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    private BuildingFunction referenceBuilding;
    public void Awake()
    {
        referenceBuilding = BuildingManager.Instance.Buildings[gameObject.transform.GetSiblingIndex()].interactionObject.GetComponent<BuildingFunction>();
    }
    //functions to detect whether the pointer is over the attached ui element
    public void OnPointerEnter(PointerEventData eventData)
    {
        UIPanels.Instance.buildingTooltipPanel.SetActive(true);
        UpdatePanel();

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        UIPanels.Instance.buildingTooltipPanel.SetActive(false);

    }

    private void UpdatePanel()
    {
        //get a reference to the tooltip panel to shorten the code a bit
        GameObject tooltipPanel = UIPanels.Instance.buildingTooltipPanel;
        //set the building name
        tooltipPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = referenceBuilding.buildingName;
        GameObject requiredItemsPanels = tooltipPanel.transform.Find("RequiredItems").gameObject;
        RequiredItem[] requiredItems = BuildingManager.Instance.Buildings[gameObject.transform.GetSiblingIndex()].RequiredItems;
        //set the required items panels
        for (int i = 0; i < 3; i++)
        {
            if(i < requiredItems.Length)
            {
                //and set the sprite and number required
                requiredItemsPanels.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = UIPanels.Instance.GetResourceSprite(requiredItems[i].resource);
                requiredItemsPanels.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.white;
                requiredItemsPanels.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = requiredItems[i].requiredAmount.ToString();
            }
            else
            {

                requiredItemsPanels.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
                requiredItemsPanels.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
        //then set the building description
        tooltipPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = referenceBuilding.buildingDescription;
    }

   
}
