using UnityEngine.UI;

public class ArmouryFunction : BuildingFunction {


    public int armourySpaceIncrease = 20;


    public override void Function()
    {
        //if the focused panel isnt null
        if(UIPanels.Instance.focusedPanel != null && UIPanels.Instance.focusedPanel != UIPanels.Instance.armouryPanel)
        {
            //deactivate it, because this buildings panel will become the focused panel
            UIPanels.Instance.focusedPanel.SetActive(false);
        }
        //if the ui panel was inactive at the time of the function being called
        if(!UIPanels.Instance.armouryPanel.activeSelf)
        {
            //activate the panel
            UIPanels.Instance.armouryPanel.SetActive(true);
            //then reset its foldout and buttons
            UIPanels.Instance.ResetPanel(UIPanels.Instance.armouryPanel.transform);
            UIPanels.Instance.focusedPanel = UIPanels.Instance.armouryPanel;
            UIPanels.Instance.armouryPanel.GetComponent<ArmouryJobPanel>().OnPanelOpen();
        }
        //otherwise it was activated
       else
        {
            //and we want to close it
            UIPanels.Instance.armouryPanel.SetActive(false);
            //and remove it as the focused panel
            UIPanels.Instance.focusedPanel = null;
        }
       
    }
    public override void OnBuilt()
    {
        Stockpile.Instance.armourySpace += armourySpaceIncrease;
        UIPanels.Instance.hudMainBar.transform.Find("BuildingButtons").Find("armoury").GetComponent<Button>().interactable = true;
    }
}
