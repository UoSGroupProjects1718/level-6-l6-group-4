using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour {

    public GameObject[] Buildings;
    public Text[] Buttons;
    private CreateBuilding createBuilding;


    // Use this for initialization
    void Start () {
        createBuilding = GetComponent<CreateBuilding>();

        //set up the buttons' text
        for(int i = 0; i < Buttons.Length;i++)
        {
            Buttons[i].text = Buildings[i].name;
        }
	}
    public void BuildingOnClick()
    {
        //get the currently clicked button
        var button = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>();
        //loop through all buildings and see if we have a name match 
        //this would need to be an integer comparison for speed in an actual game(text comparison is bad)
        for(int i = 0; i < Buildings.Length;i++)
        {
            if(Buildings[i].name == button.text)
            {
                createBuilding.SetItem(Buildings[i]);
                break;
            }
        }
    }

	
	
    //private void OnGUI()
    //{
    //    for(int i = 0; i < Buildings.Length; i++)
    //    {
    //        if(GUI.Button(new Rect(Screen.width/20, Screen.height/15 + Screen.height/12 * i, 100,30),Buildings[i].name))
    //        {
    //            createBuilding.SetItem(Buildings[i]);
    //        }
    //    }
    //}
}
