using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : SingletonClass<UIController>
{
    [SerializeField]
    private GameObject BuildingPanel;

    [SerializeField]
    private GameObject StockpilePanel;


    public void Start()
    {
        UpdateStockpile();
    }

    public void UpdateStockpile()
    {
       for(int i = 0; i < (int)ItemType.Nutrition; i++)
        {
            StockpilePanel.transform.GetChild(i).GetComponent<Text>().text = (ItemType)i +": " +  Stockpile.Instance.inventoryDictionary[(ItemType)i].ToString();
        }
        StockpilePanel.transform.GetChild(StockpilePanel.transform.childCount - 2).GetComponent<Text>().text = "Total: " + Stockpile.Instance.currResourceAmount + " / " + Stockpile.Instance.resourceSpace;
        StockpilePanel.transform.GetChild(StockpilePanel.transform.childCount - 1).GetComponent<Text>().text = "Food: " + Stockpile.Instance.inventoryDictionary[ItemType.Nutrition] + " / " + Stockpile.Instance.nutritionSpace;
    }

    //when multiple hunters are supposed to hunt one monster, multiple jobs for the same monster must be queued up, just make as many jobs as required
    public void HuntSelected()
    {
        for(int i = 0; i < UnitSelection.SelectedMonsters.Count; i++)
        {
            if(!UnitSelection.SelectedMonsters[i].beingHunted)
            {
                MonsterTypes.Instance.getNumHunters(UnitSelection.SelectedMonsters[i].monsterType, out UnitSelection.SelectedMonsters[i].numHunters);
                for(int j = 0; j < UnitSelection.SelectedMonsters[i].numHunters; j++)
                {
                    JobManager.CreateJob(JobType.Hunter, 0, UnitSelection.SelectedMonsters[i].gameObject, UnitSelection.SelectedMonsters[i].transform.position, "Hunt" + UnitSelection.SelectedMonsters[i].monsterName);
                }
             
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
