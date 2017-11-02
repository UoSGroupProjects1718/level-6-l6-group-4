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
                if(timeOfLastHarvest +CropGrowthTime/3 < TimeManager.Instance.getElapsedTime())
                {
                    currentGrowth = GrowthState.Growing;
                    //CHANGE MODEL
                }
                break;
            case GrowthState.Growing:
                if (timeOfLastHarvest + CropGrowthTime < TimeManager.Instance.getElapsedTime())
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
        timeOfLastHarvest = TimeManager.Instance.getElapsedTime();
    }

    public override void Function()
    {
        
    }

    
}
