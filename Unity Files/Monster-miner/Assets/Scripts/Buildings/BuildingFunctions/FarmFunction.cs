using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmFunction : BuildingFunction {

    enum GrowthState
    {
        JustPlanted,
        Growing,
        FullyGrown
    }

    GrowthState currentGrowth = GrowthState.JustPlanted;

    double timeOfLastHarvest;

    [SerializeField]
    float CropGrowthTime;

    public Mesh[] Models;

    // Use this for initialization
    void Start () {
        StartCoroutine(WaitForBuilt());	
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentGrowth)
        {
            case GrowthState.JustPlanted:
                if(timeOfLastHarvest +CropGrowthTime/3 < getTime())
                {
                    currentGrowth = GrowthState.Growing;
                    //CHANGE MODEL
                }
                break;
            case GrowthState.Growing:
                if (timeOfLastHarvest + CropGrowthTime < getTime())
                {
                    currentGrowth = GrowthState.FullyGrown;
                    //ADD HARVEST JOB
                    //CHANGE MODEL
                }
                break;
            default:
                break;
        }
    }

    IEnumerator WaitForBuilt()
    {
        while (!Built)
        {
            yield return new WaitForSeconds(0.5f);
        }

        currentGrowth = GrowthState.JustPlanted;
        timeOfLastHarvest = getTime();
    }

    public override void Function()
    {
        
    }

    double getTime()
    {
        return TimeManager.IngameTime.timeSinceStart;
    }
}
