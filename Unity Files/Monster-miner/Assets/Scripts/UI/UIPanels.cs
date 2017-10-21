using UnityEngine;

public class UIPanels : SingletonClass<UIPanels> {

    public GameObject BarracksPanel;
    public MonsterType BarracksTertiaryFocusedMonster;

	void Start ()
    {
        BarracksPanel = GameObject.Find("Barracks panel");
        BarracksPanel.SetActive(false);


	}
	
}
