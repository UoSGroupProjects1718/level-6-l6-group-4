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
        UIPanels.Instance.tooltipPanel.SetActive(true);
        
        UpdateTooltip();
        UIPanels.Instance.tooltipPanel.transform.position = Input.mousePosition + new Vector3(2,0,0);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //hide and reset the tooltip panel
        ResetTooltip();
        UIPanels.Instance.tooltipPanel.SetActive(false);
    }



    protected void ResetTooltip()
    {
        for(int i = 1; i < UIPanels.Instance.tooltipPanel.transform.childCount;i++)
        {
            UIPanels.Instance.tooltipPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    protected void SetElementText(int childIndex, string text)
    {
        UIPanels.Instance.tooltipPanel.transform.GetChild(childIndex).gameObject.SetActive(true);
        UIPanels.Instance.tooltipPanel.transform.GetChild(childIndex).GetComponent<TextMeshProUGUI>().text = text;
    }

    protected virtual void UpdateTooltip() { throw new System.NotImplementedException("Update tooltip not implemented"); }

}
