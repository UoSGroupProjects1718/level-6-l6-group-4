using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksFunction : BuildingFunction {


    public override void Function()
    {
        UIPanels.Instance.BarracksPanel.SetActive(!UIPanels.Instance.BarracksPanel.activeSelf);
    }
}
