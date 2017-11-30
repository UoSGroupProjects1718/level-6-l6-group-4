using UnityEngine;
using UnityEngine.UI;
public class UIPanels : SingletonClass<UIPanels> {
    [Header("The point on the screen each panel is docked")]
    [SerializeField]
    private Vector2 panelSharedAnchorPoint;


    //barracks info
    [HideInInspector]
    public InputField barracksInputField;
    [HideInInspector]
    public GameObject barracksPanel;
    [HideInInspector]
    public MonsterType barracksTertiaryFocusedMonster;

    //armoury info
    [HideInInspector]
    public GameObject armouryPanel;

    //blacksmith info
    [HideInInspector]
    public GameObject blacksmithPanel;
    [HideInInspector]
    public InputField blacksmithInputField;
    [HideInInspector]
    public GameObject blacksmithFocusedJob;
    public Craftable[] blacksmithCraftingRecipes;

    [HideInInspector]
    public GameObject focusedPanel;


    [SerializeField]
    private Sprite[] resourceSprites;



	public override void Awake ()
    {
        base.Awake();
        //uncomment on barracks reimplementation
        //barracksPanel = GameObject.Find("Barracks panel");
        //barracksInputField = barracksPanel.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<InputField>();
        //ResetPanel(barracksPanel.transform);
        //barracksPanel.SetActive(false);

        armouryPanel = GameObject.Find("Armoury panel");
        ResetPanel(armouryPanel.transform);
        armouryPanel.SetActive(false);

        blacksmithPanel = GameObject.Find("Blacksmith panel");
        blacksmithInputField = blacksmithPanel.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<InputField>();
        ResetPanel(blacksmithPanel.transform);
        blacksmithPanel.SetActive(false);
    }


    public void ResetPanel(Transform panel)
    {
        panel.GetChild(1).gameObject.SetActive(false);
        panel.GetChild(2).gameObject.SetActive(false);
        panel.GetComponent<RectTransform>().anchoredPosition = panelSharedAnchorPoint;
    }

    public Sprite GetResourceSprite(ItemType resourceType)
    {
        switch (resourceType)
        {
            case ItemType.Wood:
                return resourceSprites[0];
            case ItemType.Stone:
                return resourceSprites[1];
            case ItemType.Iron:
                return resourceSprites[2];
            case ItemType.Bone:
                return resourceSprites[3];
            case ItemType.Crystal:
                return resourceSprites[4];
            case ItemType.Nutrition:
                return resourceSprites[5];
            default:
                return null;
        }
    }
}
