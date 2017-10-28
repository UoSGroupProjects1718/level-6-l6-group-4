using UnityEngine;
using UnityEngine.UI;
public class UIPanels : SingletonClass<UIPanels> {


    public InputField BarracksInputField;
    public GameObject BarracksPanel;
    public MonsterType BarracksTertiaryFocusedMonster;

	public override void Awake ()
    {
        base.Awake();
        BarracksPanel = GameObject.Find("Barracks panel");
        BarracksPanel.transform.GetChild(1).gameObject.SetActive(false);
        BarracksPanel.transform.GetChild(3).gameObject.SetActive(false);
        BarracksInputField = BarracksPanel.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<InputField>();
        BarracksPanel.SetActive(false);


    }



}
