using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranaryFunction : BuildingFunction {

    public int ResourceSpaceIncrease;

    public override void Function()
    {
        //show ui elements on click
    }

    public override void OnBuilt()
    {
        Stockpile.Instance.nutritionSpace += ResourceSpaceIncrease;
    }


}
