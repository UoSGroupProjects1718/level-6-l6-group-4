using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonistController : MonoBehaviour {
    public MonsterMiner.BehaviourTree.BehaviourBase BTHead;
	// Use this for initialization
	void Start () {
        BTHead.UpdateFunc(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
