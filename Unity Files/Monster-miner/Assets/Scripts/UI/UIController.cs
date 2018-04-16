using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : SingletonClass<UIController>
{
    [SerializeField]
    private GameObject buildingPanel;

    [SerializeField]
    private GameObject stockpilePanel;

    [SerializeField]
    public GameObject colonistInfoPanel;

    [HideInInspector]
    public ColonistController focusedColonist;

    public void Start()
    {
        UpdateStockpile();
    }

    public void Update()
    {
        //if the player clicks
        if(Input.GetKeyDown(Keybinds.Instance.PrimaryActionKey))
        {
            //send out a ray
            RaycastHit rayhit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out rayhit))
            {
                //and if they hit something
                if (rayhit.collider != null)
                {
                    //check to see if there is a colonist controller attached to the object
                    ColonistController colonist = rayhit.collider.GetComponent<ColonistController>();
                    //if there is
                    if(colonist != null)
                    {
                        //then set that colonist as the focus of the colonist panel
                        colonistInfoPanel.SetActive(true);
                        focusedColonist = colonist;
                        UpdateColonistInfoPanel(colonist);
                    }
                    else
                    {
                        //otherwise deactivate the panel
                        colonistInfoPanel.SetActive(false);
                    }
                }
            }
        }
    }
    public void UpdateColonistInfoPanel(ColonistController colonist)
    {
        //first set the most basic information
        colonistInfoPanel.transform.Find("Name").GetComponent<Text>().text = colonist.colonistName;
        colonistInfoPanel.transform.Find("Job type").GetComponent<Text>().text = colonist.colonistJob.ToString();
        //then set the armour sprites
        Equipment colonistEquipment = colonist.GetComponent<Equipment>();
        if(colonistEquipment != null)
        {
            colonistInfoPanel.transform.Find("Armour/Chest").GetComponent<Image>().sprite = colonistEquipment.equippedArmour[(int)ArmourSlot.Torso].uiSprite;
            colonistInfoPanel.transform.Find("Armour/Legs").GetComponent<Image>().sprite = colonistEquipment.equippedArmour[(int)ArmourSlot.Legs].uiSprite;
            colonistInfoPanel.transform.Find("Armour/Weapon").GetComponent<Image>().sprite = colonistEquipment.weapon.uiSprite;
        }

        //then set the other information
        colonistInfoPanel.transform.Find("TextStats/Health").GetComponent<Text>().text = "Health: " + colonist.health.ToString();
        if(colonist.currentJob != null)
        {
            colonistInfoPanel.transform.Find("TextStats/CurrentJob").GetComponent<Text>().text = "Job: " + colonist.currentJob.jobName;
        }
        else
        {
            colonistInfoPanel.transform.Find("TextStats/CurrentJob").GetComponent<Text>().text = "Job: ";
        }
        colonistInfoPanel.transform.Find("TextStats/Move speed").GetComponent<Text>().text = "Movement speed: " + colonist.colonistMoveSpeed.ToString();
        colonistInfoPanel.transform.Find("TextStats/Food requirement").GetComponent<Text>().text = "Food requirement: " + colonist.requiredNutritionPerDay.ToString();
        colonistInfoPanel.transform.Find("TextStats/Damage reduction").GetComponent<Text>().text = "Armour: " + colonistEquipment.damageReduction.ToString();
        colonistInfoPanel.transform.Find("TextStats/Attack speed").GetComponent<Text>().text = "Attack speed: " + colonistEquipment.weapon.AttackSpeed.ToString();
        colonistInfoPanel.transform.Find("TextStats/Weapon damage").GetComponent<Text>().text = "Weapon damage: " + colonistEquipment.weapon.Damage.ToString();
        colonistInfoPanel.transform.Find("TextStats/Weapon range").GetComponent<Text>().text = "Weapon range: " + colonistEquipment.weapon.Range.ToString();

    }
    public void UpdateStockpile()
    {
        //reset the current resource amount
        Stockpile.Instance.currResourceAmount = 0;

       for(int i = 0; i < (int)ItemType.Nutrition; i++)
        {
            stockpilePanel.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = (ItemType)i +": " +  Stockpile.Instance.inventoryDictionary[(ItemType)i].ToString();
            Stockpile.Instance.currResourceAmount += Stockpile.Instance.inventoryDictionary[(ItemType)i];
        }
        stockpilePanel.transform.GetChild(stockpilePanel.transform.childCount - 2).GetComponent<TextMeshProUGUI>().text = "Total: " + Stockpile.Instance.currResourceAmount + " / " + Stockpile.Instance.resourceSpace;
        stockpilePanel.transform.GetChild(stockpilePanel.transform.childCount - 1).GetComponent<TextMeshProUGUI>().text = "Food: " + Stockpile.Instance.inventoryDictionary[ItemType.Nutrition] + " / " + Stockpile.Instance.nutritionSpace;
    }

    //when multiple hunters are supposed to hunt one monster, multiple jobs for the same monster must be queued up, just make as many jobs as required
    public void HuntSelected()
    {
        for(int i = 0; i < UnitSelection.SelectedMonsters.Count; i++)
        {
            if(!UnitSelection.SelectedMonsters[i].beingHunted)
            {
                MonsterTypes.Instance.getNumHunters(UnitSelection.SelectedMonsters[i], out UnitSelection.SelectedMonsters[i].numHunters);
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
        buildingPanel.SetActive(!buildingPanel.activeSelf);
    }
    public void BarracksTertiaryActivate()
    {
        UIPanels.Instance.barracksPanel.transform.GetChild(1).gameObject.SetActive(!UIPanels.Instance.barracksPanel.transform.GetChild(1).gameObject.activeSelf);
    }
    public void ReturnFromHuntOnClick()
    {
        for(int i = 0; i < UnitSelection.SelectedColonists.Count; i++)
        {
            if(UnitSelection.SelectedColonists[i].currentJob.jobType == JobType.Hunter)
            {
                UnitSelection.SelectedColonists[i].currentJob = null;
            }
        }
    }
   
    public void IncrementBarracksInputField()
    {
        int number = int.Parse(UIPanels.Instance.barracksInputField.text);
        number++;
        UIPanels.Instance.barracksInputField.text = number.ToString();
    }
    public void DecrementBarracksInputField()
    {
        int number = int.Parse(UIPanels.Instance.barracksInputField.text);
        if(number > 1)
        {
            number--;
            UIPanels.Instance.barracksInputField.text = number.ToString();
        }
    }
    public void  BarracksConfirmButton()
    {
        UIPanels.Instance.barracksTertiaryFocusedMonster.numHuntersRequired = int.Parse(UIPanels.Instance.barracksInputField.text);
    }
}
