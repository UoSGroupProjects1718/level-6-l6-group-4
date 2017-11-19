
public class ArmouryFunction : BuildingFunction {

    public override void Function()
    {
        UIPanels.Instance.ArmouryPanel.SetActive(!UIPanels.Instance.ArmouryPanel.activeSelf);
        if (UIPanels.Instance.ArmouryPanel.activeSelf)
            UIPanels.Instance.ArmouryPanel.GetComponent<ArmouryJobPanel>().OnPanelOpen();
       
    }
}
