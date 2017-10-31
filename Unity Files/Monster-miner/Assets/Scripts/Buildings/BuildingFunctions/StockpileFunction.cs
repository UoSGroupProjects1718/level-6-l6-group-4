using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockpileFunction : BuildingFunction
{
    public int ResourceIncreaseAmount;
 

    public override void Function()
    {
        //show ui elements on click
    }
    public override void OnBuilt()
    {
        Stockpile.Instance.ResourceSpace += ResourceIncreaseAmount;
    }


}
