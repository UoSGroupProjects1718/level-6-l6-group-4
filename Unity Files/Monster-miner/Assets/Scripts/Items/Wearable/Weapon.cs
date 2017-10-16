using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/ Weapon")]
public class Weapon : Wearable {

    public float Damage;
    [Header("Range in unity units")]
    public float Range;
    [Header("Seconds between attacks")]
    public float AttackSpeed;
    [Range(1,100)]
    public float Accuracy;

}
