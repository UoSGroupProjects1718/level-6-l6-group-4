using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmourSlot
{
    Head,
    Torso,
    Legs,
}

[CreateAssetMenu(menuName = ("Items/Armour"))]
public class Armour : Wearable
{

    public ArmourSlot armourSlot;

    [Range(0,100)]
    public float DamageReduction;

}
