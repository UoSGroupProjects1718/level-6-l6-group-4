using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = ("Items/Armour"))]
public class Armour : Wearable
{  

    [Range(0,100)]
    public float DamageReduction;

}
