using UnityEngine.UI;
using UnityEngine;

public class HUDMainBar : MonoBehaviour {

    public void Start()
    {
        GameObject buildingButtons = transform.Find("BuildingButtons").gameObject;
        buildingButtons.transform.Find("blacksmith").GetComponent<Button>().interactable = false;
        buildingButtons.transform.Find("armoury").GetComponent<Button>().interactable = false;
        buildingButtons.transform.Find("barracks").GetComponent<Button>().interactable = false;
    }

    public void ToggleInventory()
    {
        UIController.Instance.stockpilePanel.transform.parent.gameObject.SetActive(!UIController.Instance.stockpilePanel.transform.parent.gameObject.activeSelf);
    }
    public void ToggleBuildingPanel()
    {
        UIPanels.Instance.buildingPanel.SetActive(!UIPanels.Instance.buildingPanel.activeSelf);
    }
    public void ShowArmouryPanel()
    {
        BehaviourTreeManager.Armouries[0].Function();
    }
    public void ShowBlacksmithPanel()
    {
        BehaviourTreeManager.Blacksmiths[0].Function();
    }
    public void ShowBarracksPanel()
    {
        BehaviourTreeManager.Barracks[0].Function();
    }
}
