using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseFunction : BuildingFunction {


    [HideInInspector]
    public bool colonistsSpawned;

    public int colonistsToSpawn = 4;

    public override void Function()
    {
        
    }
    public override void OnBuilt()
    {
        AlertsManager.Instance.CreateAlert("House has been completed, click to confirm", AlertType.HouseCompletion);
    }
    public void SpawnColonists(int hunters, int gatherers, int crafters)
    {
       
        //spawn the required number of hunters
        for (int h = 0; h < hunters; h++)
        {
            ColonistController colonist = ColonistSpawner.Instance.SpawnColonist(transform.position, ColonistJobType.Hunter);

        }
        //spawn the required number of scouts
        for (int s = 0; s < gatherers; s++)
        {
            ColonistController colonist = ColonistSpawner.Instance.SpawnColonist(transform.position, ColonistJobType.Scout);

        }
        //spawn the required number of crafters
        for (int c = 0; c < crafters; c++)
        {
            ColonistController colonist = ColonistSpawner.Instance.SpawnColonist(transform.position, ColonistJobType.Crafter);

        }
        //then return the recently used button
        AlertsManager.Instance.ReturnAlertButton(UIPanels.Instance.alertsHolder.GetChild(AlertsManager.Instance.currentAlertButtonIndex).gameObject);
    }
}
