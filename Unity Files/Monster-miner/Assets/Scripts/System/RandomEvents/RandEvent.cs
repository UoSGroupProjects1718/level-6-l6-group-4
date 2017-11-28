using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandEvent : ScriptableObject {

    public Vector2 timeToActivate;
	// Use this for initialization
    protected virtual void Event() { }

    public IEnumerator CallEvent() {
        Debug.Log("AAAAAAAAAAAAAAAAa");
        while(!(TimeManager.Instance.IngameTime.hours==timeToActivate.x && Mathf.Abs(TimeManager.Instance.IngameTime.minutes- timeToActivate.y)  < 5)) {
            yield return new WaitForFixedUpdate();
        }
        Event();
        Debug.Log("EVENT! AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        yield break;
    }

}
