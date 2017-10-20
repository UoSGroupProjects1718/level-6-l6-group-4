using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	
}
