using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : Effect
{
    public override void Function() 
    {
        //do a sphere cast to get everything in range
        RaycastHit[] enemiesHit = Physics.SphereCastAll(transform.position, connectedTrap.range, Vector3.forward, enemyMask);

        for (int i = 0; i < enemiesHit.Length; i++)
        {
            //then loop through all hits and damage them
            enemiesHit[i].collider.GetComponent<EnemyHealth>().health -= connectedTrap.Damage;
        }
    }

	
}
