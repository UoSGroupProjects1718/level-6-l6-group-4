using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/ Weapon")]
public class Weapon : ItemInfo {

    public float Damage;
    [Header("Range in unity units")]
    public float Range;
    [Header("Seconds between attacks")]
    public float AttackSpeed;

}
