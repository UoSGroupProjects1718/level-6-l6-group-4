using UnityEngine;
using UnityEngine.UI;
public class UIPanels : SingletonClass<UIPanels> {

    [SerializeField]
    private Vector2 panelSharedAnchorPoint;
    [HideInInspector]
    public InputField barracksInputField;
    [HideInInspector]
    public GameObject barracksPanel;
    [HideInInspector]
    public MonsterType barracksTertiaryFocusedMonster;
    [HideInInspector]
    public GameObject armouryPanel;

    [HideInInspector]
    public GameObject focusedPanel;

	public override void Awake ()
    {
        base.Awake();
        barracksPanel = GameObject.Find("Barracks panel");
        barracksInputField = barracksPanel.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<InputField>();
        ResetPanel(barracksPanel.transform);
        barracksPanel.SetActive(false);

        armouryPanel = GameObject.Find("Armoury panel");
        ResetPanel(armouryPanel.transform);
        armouryPanel.SetActive(false);
    }


    public void ResetPanel(Transform panel)
    {
        panel.GetChild(1).gameObject.SetActive(false);
        panel.GetChild(3).gameObject.SetActive(false);
        panel.GetChild(2).gameObject.SetActive(true);
        panel.GetComponent<RectTransform>().anchoredPosition = panelSharedAnchorPoint;
    }
}
