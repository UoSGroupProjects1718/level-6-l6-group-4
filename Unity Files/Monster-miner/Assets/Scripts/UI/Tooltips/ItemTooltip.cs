using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {



    [HideInInspector]
    public ItemInfo referenceItem;

  

    //functions to detect whether the pointer is over the attached ui element
    public void OnPointerEnter(PointerEventData eventData)
    {
        //show the tooltip panel and update it
        UIPanels.Instance.blacksmithTooltipPanel.SetActive(true);
        
        UpdateTooltip();
       
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //hide and reset the tooltip panel
        ResetTooltip();
        UIPanels.Instance.blacksmithTooltipPanel.SetActive(false);
    }



    protected void ResetTooltip()
    {
        for(int i = 1; i < UIPanels.Instance.blacksmithTooltipPanel.transform.childCount;i++)
        {
            UIPanels.Instance.blacksmithTooltipPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    protected void SetElementText(int childIndex, string text)
    {
        UIPanels.Instance.blacksmithTooltipPanel.transform.GetChild(childIndex).gameObject.SetActive(true);
        UIPanels.Instance.blacksmithTooltipPanel.transform.GetChild(childIndex).GetComponent<TextMeshProUGUI>().text = text;
    }

    protected virtual void UpdateTooltip() { throw new System.NotImplementedException("Update tooltip not implemented"); }

}
