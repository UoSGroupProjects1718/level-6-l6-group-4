using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandEvent : MonoBehaviour {

    public Vector2 timeToActivate;
	// Use this for initialization
    protected virtual void Event() { }

    public IEnumerator CallEvent() {
        while(!(TimeManager.Instance.IngameTime.hours==timeToActivate.x && TimeManager.Instance.IngameTime.minutes == timeToActivate.y)) {
            yield return new WaitForFixedUpdate();
        }
        Event();
        yield break;
    }

}
