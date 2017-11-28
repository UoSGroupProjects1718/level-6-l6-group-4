using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Random Event/Spawn Monster Group")]
public class SpawnMonsterGroup : RandEvent {

    public GameObject Raptors;
    public float DistFromTown;

    protected override void Event()
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);

        Vector3 pos = new Vector3(Mathf.Sin(angle) * DistFromTown, float.MaxValue, Mathf.Cos(angle) * DistFromTown);
        pos.y = GetYPoint(pos);
        Instantiate(Raptors, pos, Quaternion.identity);
    }


    float GetYPoint(Vector3 rayPos)
    {
        Ray ray = new Ray(rayPos, new Vector3(0, -1, 0));
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit.point.y;
    }
}
