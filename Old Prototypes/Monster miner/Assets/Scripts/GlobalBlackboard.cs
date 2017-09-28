using UnityEngine;
using System.Collections.Generic;
using System.Collections;

    public enum JobType
    {
        Build,
        Labourer,
    }

public class GlobalBlackboard : MonoBehaviour {

    public static List<AgentInfo> Agents;
    public static List<Job> JobDocket;

    public static Grid pathfindingGrid;

    public void Awake()
    {
        Agents = new List<AgentInfo>();
        JobDocket = new List<Job>();
        pathfindingGrid = FindObjectOfType<Grid>();
    }
    public void Start()
    {
        StartCoroutine(AgentTick());
    }
    private IEnumerator AgentTick()
    {
        while(true)
        {
            for (int i = 0; i < Agents.Count; i++)
            {
                //again, very hacky code from the AgentBehaviour tree and related scripts
                if (Agents[i].BT.sel != null)
                    Agents[i].BT.sel.tick();
                else
                    Agents[i].BT.seq.tick();
            }
            yield return null;
        }
    }
}
