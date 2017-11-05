using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvents : MonoBehaviour {
    [SerializeField]
    List<RandEvent> eventList;
    bool checkedToday=false;
    [SerializeField]
    float percentageChanceOfEvent;
    
	// Update is called once per frame
	void FixedUpdate () {
        Debug.Log(this.name);
		if(TimeManager.Instance.IngameTime.hours==0 && TimeManager.Instance.IngameTime.minutes == 0 && checkedToday==false)//check every day for a random event
        {
            if(Random.Range(0,100) < percentageChanceOfEvent)
            {
                StartCoroutine(RandomEvent());
            }
        }
	}

    IEnumerator RandomEvent()
    {
        checkedToday = true;

        if (eventList.Count > 0)
        {
            StartCoroutine(eventList[Random.Range(0, eventList.Count)].CallEvent());
        }

        while (TimeManager.Instance.IngameTime.minutes == 0)
        {
            continue;
        }

        checkedToday = false;
        return null;
    }
}
