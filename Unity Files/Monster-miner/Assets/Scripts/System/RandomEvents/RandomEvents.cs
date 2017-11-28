using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvents : MonoBehaviour {
    [SerializeField]
    List<RandEvent> eventList = new List<RandEvent>();
    bool checkedToday=false;
    [SerializeField,Range(0,100)]
    float percentageChanceOfEvent = 0;
    
	// Update is called once per frame
	void FixedUpdate () {
		if(TimeManager.Instance.IngameTime.hours==0 && TimeManager.Instance.IngameTime.minutes< 5 && !checkedToday)//check every day for a random event
        {
            checkedToday = true;
            StartCoroutine(delayChecked());
            if(Random.Range(0,100f) < percentageChanceOfEvent)
            {
                if (eventList.Count > 0)
                {
                    StartCoroutine(eventList[Random.Range(0, eventList.Count)].CallEvent());
                }
            }
        }
	}


    IEnumerator delayChecked()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        checkedToday = false;
    }
}
