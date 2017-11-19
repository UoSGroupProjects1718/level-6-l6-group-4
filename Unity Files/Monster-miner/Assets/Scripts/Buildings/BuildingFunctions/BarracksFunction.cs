
public class BarracksFunction : BuildingFunction {

    public override void Function()
    {
        UIPanels.Instance.BarracksPanel.SetActive(!UIPanels.Instance.BarracksPanel.activeSelf);
        
    }
}
