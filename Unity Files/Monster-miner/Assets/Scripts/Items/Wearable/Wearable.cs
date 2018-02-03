using UnityEngine;

public enum ArmourSlot
{
    Head,
    Torso,
    Legs,
    Weapon,
}
public class Wearable : ItemInfo {

    // public SkinnedMeshRenderer equippableMesh;
    public ArmourSlot armourSlot;
    public float walkSpeedModifier;
    public float workSpeedModifier;
}
