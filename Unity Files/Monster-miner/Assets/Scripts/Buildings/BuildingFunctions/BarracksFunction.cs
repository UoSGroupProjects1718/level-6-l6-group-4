
public class BarracksFunction : BuildingFunction {

    public override void Function()
    {
        //if there is a focused panel, deactivate it
        if(UIPanels.Instance.focusedPanel != null && UIPanels.Instance.focusedPanel != UIPanels.Instance.barracksPanel)
        {
            UIPanels.Instance.focusedPanel.SetActive(false);
        }
        //if the panel was inactive at the time of the function being called
        if(!UIPanels.Instance.barracksPanel.activeSelf)
        {
            UIPanels.Instance.barracksPanel.SetActive(true);
            UIPanels.Instance.ResetPanel(UIPanels.Instance.barracksPanel.transform);
            UIPanels.Instance.focusedPanel = UIPanels.Instance.barracksPanel;
        }
        //otherwise it was activated
        else
        {
            //and we want to close it
            UIPanels.Instance.barracksPanel.SetActive(false);
            //and remove it as the focused panel
            UIPanels.Instance.focusedPanel = null;
        }
    }
}