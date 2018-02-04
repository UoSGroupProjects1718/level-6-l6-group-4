using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum HousePanelEditMode
{
    Hunter,
    Scout,
    Crafter,
}


public class HouseUI : MonoBehaviour {

    [HideInInspector]
    public HouseFunction focusedHouse;

    private InputField[] inputFields;


    HousePanelEditMode mode;

    int hunter, scout, crafter;

    public void Awake()
    {
        inputFields = GetComponentsInChildren<InputField>();
    }
    public void OnEnable()
    {
        ResetHouseUI();
    }
    public void ChangeEditMode(int e)
    {
        mode = (HousePanelEditMode)e;
    }

    //depending on the mode, add the value to the specified int, and then validate that number
    public void ChangeValue(int val)
    {
        switch (mode)
        {
            case HousePanelEditMode.Hunter:
                hunter += val;
                ValidateNumber(ref hunter);
                break;
            case HousePanelEditMode.Scout:
                scout += val;
                ValidateNumber(ref scout);
                break;
            case HousePanelEditMode.Crafter:
                crafter += val;
                ValidateNumber(ref crafter);
                break;

            default:
                break;
        }
    }
    //update the correct input field with the required number
    public void UpdateInputField()
    {
        int val = 0;
        switch (mode)
        {
            case HousePanelEditMode.Hunter:
                val = hunter;
                break;
            case HousePanelEditMode.Scout:
                val = scout;
                break;
            case HousePanelEditMode.Crafter:
                val = crafter;
                break;
            default:
                break;
        }
        inputFields[(int)mode].text = val.ToString();
    }

    
    private void ValidateNumber(ref int val)
    {
        //if the value is less than 0, return it to zero
        if (val < 0)
            val = 0;


        int sum = 0;
        int maxAllowed = 0;
        //depending on which editmode we are in, we want to sum different values
        switch (mode)
        {
            case HousePanelEditMode.Hunter:
                sum = scout + crafter;
                break;
            case HousePanelEditMode.Scout:
                sum = hunter + crafter;
                break;
            case HousePanelEditMode.Crafter:
                sum = hunter + scout;
                break;
            default:
                break;
        }
        //then basedon the summed values, figure out how much the current value can be.
        maxAllowed = UIPanels.Instance.currHouseColonistAmount - sum;

        //then if it is above that value, make it the max possible value.
        if (val > maxAllowed)
            val = maxAllowed;

        //update the total text to show the currently queued requested jobs
        int currentTotal = scout + hunter + crafter;
        UIPanels.Instance.houseCompletionPanel.transform.Find("TotalChosenText").GetComponent<Text>().text = currentTotal.ToString() + "/" + UIPanels.Instance.currHouseColonistAmount;

        //and check to see if the confirm button should be active
        if (scout+hunter+crafter == UIPanels.Instance.currHouseColonistAmount)
        {
            UIPanels.Instance.houseCompletionPanel.transform.Find("ConfirmButton").GetComponent<Button>().interactable = true;
        }
        else
        {
            UIPanels.Instance.houseCompletionPanel.transform.Find("ConfirmButton").GetComponent<Button>().interactable = false;
        }



    }



    public void ConfirmButton()
    {
        //spawn the required number of hunters
        for(int h = 0; h < hunter; h++)
        {
            ColonistController colonist =ColonistSpawner.Instance.SpawnColonist(focusedHouse.transform.position,ColonistJobType.Hunter);

        }
        //spawn the required number of scouts
        for(int s = 0; s < scout; s++)
        {
            ColonistController colonist = ColonistSpawner.Instance.SpawnColonist(focusedHouse.transform.position, ColonistJobType.Scout);

        }
        //spawn the required number of crafters
        for(int c = 0; c < crafter; c++)
        {
            ColonistController colonist = ColonistSpawner.Instance.SpawnColonist(focusedHouse.transform.position, ColonistJobType.Crafter);

        }
        //then return the recently used button
        AlertsManager.Instance.ReturnAlertButton(UIPanels.Instance.alertsHolder.GetChild(AlertsManager.Instance.currentAlertButtonIndex).gameObject);
        //and set the focused house to having spawned colonists
        focusedHouse.colonistsSpawned = true;
    }

    public void ResetHouseUI()
    {
        //reset the desired job variables
        hunter = 0;
        scout = 0;
        crafter = 0;

        for(int i = 0; i < inputFields.Length; i++)
        {
            inputFields[i].text = 0.ToString();
        }

        UIPanels.Instance.houseCompletionPanel.transform.Find("ConfirmButton").GetComponent<Button>().interactable = false;
        UIPanels.Instance.houseCompletionPanel.transform.Find("TotalChosenText").GetComponent<Text>().text = 0.ToString() + "/" + UIPanels.Instance.currHouseColonistAmount;

    }
}
