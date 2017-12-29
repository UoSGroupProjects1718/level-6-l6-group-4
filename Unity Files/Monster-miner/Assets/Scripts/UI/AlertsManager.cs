using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public enum AlertType
{
    Event,
    HouseCompletion,
}

public class AlertsManager : SingletonClass<AlertsManager>
{

    private Transform alertsContentParent;

    private Transform alertsButtonPoolParent;
    [HideInInspector]
    public int currentAlertButtonIndex;

    [SerializeField]
    private int buttonPoolSize = 20;
    [SerializeField]
    private GameObject alertButtonPrefab;

    public override void Awake()
    {
        base.Awake();


        alertsContentParent = GameObject.Find("Alerts").transform.GetChild(0);

        //create the button pool.
        alertsButtonPoolParent = new GameObject().transform;
        alertsButtonPoolParent.gameObject.name = "alertsButtonPool";

        for(int i = 0; i < buttonPoolSize; i++)
        {
            GameObject alertButton = Instantiate(alertButtonPrefab,alertsButtonPoolParent);
            alertButton.SetActive(false);
        }
    }

    public void CreateAlert(string alertText, AlertType alertType)
    {
        GameObject alertButton = Instance.FindAlertButton();//try to find a new button and have it added to the stack of buttons
        if(alertButton)//if we did find one
        {
            alertButton.GetComponent<Button>().onClick.RemoveAllListeners();//remove all listeners from the button

            switch(alertType)//then based on what alert type we require
            {
                case AlertType.Event:
                    alertButton.GetComponent<Button>().onClick.AddListener(delegate {ShowTextAlert(alertText);});//if an event is needed, add the text alert box
                    break;

                case AlertType.HouseCompletion:
                    alertButton.GetComponent<Button>().onClick.AddListener(ShowHouseCompletion);//or if house completion is needed, show that
                    break;

                default:
                    break;


            }

        }
    }

    public void ShowTextAlert(string alertText)
    {
        //get the current button index
        currentAlertButtonIndex = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();
        //set the required text component
        UIPanels.Instance.textAlertPanel.transform.GetChild(0).GetComponent<Text>().text = alertText;
        //and activate the panel
        UIPanels.Instance.textAlertPanel.SetActive(true);
    }
    public void ShowHouseCompletion()
    {
        //get the currnent button index
        currentAlertButtonIndex = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();
        //and activate it
        UIPanels.Instance.houseCompletionPanel.SetActive(true);
    }



    //activates the first alert button in the pool, then changes its transform parent to the alerts pane and returns it.
    //because of how the pool works (the pool is essentially just a game object with a set of children), first we find a child and then
    //we change its transform parent, thus removing it from the pool, we only want to find if there is in fact a button in the pool
    public GameObject FindAlertButton()
    {
        //find the first button
        GameObject alertButton = alertsButtonPoolParent.GetChild(0).gameObject;
        //if a button was found
        if (alertButton != null)
        {
            //make it active
            alertButton.SetActive(true);
            //and set its transform parent
            alertButton.transform.parent = alertsContentParent;
            alertButton.transform.localScale = Vector2.one;
            return alertButton;
        }
        return null;
    }
    //returns the specified alert button to the pool.
    public void ReturnAlertButton(GameObject alertButton)
    {
        alertButton.SetActive(false);
        alertButton.transform.parent = alertsButtonPoolParent;
    }
    
    
}
