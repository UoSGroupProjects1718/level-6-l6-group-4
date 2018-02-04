using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseFunction : BuildingFunction {

    [HideInInspector]
    public bool colonistsSpawned;

    public override void Function()
    {
        
    }
    public override void OnBuilt()
    {
        AlertsManager.Instance.CreateAlert("House has been completed, click to confirm", AlertType.HouseCompletion);
    }
}
