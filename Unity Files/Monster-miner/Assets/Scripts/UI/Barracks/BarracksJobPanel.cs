using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarracksJobPanel : MonoBehaviour
{
    [HideInInspector]
    public MonsterType monster;

    [HideInInspector]
    private GameObject TertiaryPanel;

    public void Start()
    {
        TertiaryPanel = UIPanels.Instance.BarracksPanel.transform.GetChild(1).gameObject;
    }

    public void OnMouseDown()
    {
        if(TertiaryPanel.activeSelf)
        {
            //TertiaryPanel.transform.GetChild(0).GetComponent<Image>().sprite = monster.UISprite;
            TertiaryPanel.transform.GetChild(1).GetComponent<Text>().text = monster.monsterName;
        }
        else
        {
            TertiaryPanel.SetActive(true);
        }
    }
}
