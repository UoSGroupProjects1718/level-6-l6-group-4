using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarracksJobPanel : MonoBehaviour
{
   // [HideInInspector]
    public MonsterType monster;

    [HideInInspector]
    private GameObject TertiaryPanel;

    public void Start()
    {
        TertiaryPanel = UIPanels.Instance.BarracksPanel.transform.GetChild(1).gameObject;
    }

    public void MouseDown()
    {
        if(TertiaryPanel.activeSelf)
        {
            //TertiaryPanel.transform.GetChild(0).GetComponent<Image>().sprite = monster.UISprite;
            TertiaryPanel.transform.GetChild(1).GetComponent<Text>().text = monster.monsterName;
            UIPanels.Instance.BarracksTertiaryFocusedMonster = monster;
            UIPanels.Instance.BarracksInputField.text = monster.numHuntersRequired.ToString();
        }
        else
        {
            TertiaryPanel.transform.GetChild(1).GetComponent<Text>().text = monster.monsterName;
            UIPanels.Instance.BarracksPanel.transform.GetChild(2).gameObject.SetActive(false);
            UIPanels.Instance.BarracksPanel.transform.GetChild(3).gameObject.SetActive(true);
            UIPanels.Instance.BarracksTertiaryFocusedMonster = monster;
            UIPanels.Instance.BarracksInputField.text = monster.numHuntersRequired.ToString();
            TertiaryPanel.SetActive(true);
        }
    }
}
