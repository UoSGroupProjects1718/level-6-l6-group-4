using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject BuildingPanel;

    //when multiple hunters are supposed to hunt one monster, multiple jobs for the same monster must be queued up, just make as many jobs as required
    public void HuntSelected()
    {
        for(int i = 0; i < UnitSelection.SelectedMonsters.Count; i++)
        {
            if(!UnitSelection.SelectedMonsters[i].beingHunted)
            {
                JobManager.CreateJob(JobType.Hunter, 0, UnitSelection.SelectedMonsters[i].gameObject, UnitSelection.SelectedMonsters[i].transform.position, "Hunt" + UnitSelection.SelectedMonsters[i].monsterName);
                UnitSelection.SelectedMonsters[i].beingHunted = true;
            }
        }
    }
    public void ToggleBuildingPanel()
    {
        BuildingPanel.SetActive(!BuildingPanel.activeSelf);
    }
    public void BarracksTertiaryActivate()
    {
        UIPanels.Instance.BarracksPanel.transform.GetChild(1).gameObject.SetActive(!UIPanels.Instance.BarracksPanel.transform.GetChild(1).gameObject.activeSelf);
    }

    //this is disgusting code, needs to be revisited
    public void IncrementBarracksInputField()
    {
        int number = int.Parse(UIPanels.Instance.BarracksInputField.text);
        number++;
        UIPanels.Instance.BarracksInputField.text = number.ToString();
    }
    public void DecrementBarracksInputField()
    {
        int number = int.Parse(UIPanels.Instance.BarracksInputField.text);
        if(number > 1)
        {
            number--;
            UIPanels.Instance.BarracksInputField.text = number.ToString();
        }
    }
    public void  BarracksConfirmButton()
    {
        UIPanels.Instance.BarracksTertiaryFocusedMonster.numHuntersRequired = int.Parse(UIPanels.Instance.BarracksInputField.text);
    }
}
