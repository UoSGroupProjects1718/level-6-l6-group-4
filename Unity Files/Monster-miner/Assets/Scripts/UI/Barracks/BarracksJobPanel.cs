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
        TertiaryPanel = UIPanels.Instance.barracksPanel.transform.GetChild(1).gameObject;
    }

    public void MouseDown()
    {
        if(TertiaryPanel.activeSelf)
        {
            //TertiaryPanel.transform.GetChild(0).GetComponent<Image>().sprite = monster.UISprite;
            TertiaryPanel.transform.GetChild(1).GetComponent<Text>().text = monster.monsterName;
            UIPanels.Instance.barracksTertiaryFocusedMonster = monster;
            UIPanels.Instance.barracksInputField.text = monster.numHuntersRequired.ToString();
        }
        else
        {
            TertiaryPanel.transform.GetChild(1).GetComponent<Text>().text = monster.monsterName;
            UIPanels.Instance.barracksPanel.transform.GetChild(2).gameObject.SetActive(true);
            UIPanels.Instance.barracksTertiaryFocusedMonster = monster;
            UIPanels.Instance.barracksInputField.text = monster.numHuntersRequired.ToString();
            TertiaryPanel.SetActive(true);
        }
    }
}
