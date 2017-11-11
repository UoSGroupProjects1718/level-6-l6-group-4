using UnityEngine;
using System;


public class Equipment : MonoBehaviour
{
    public Weapon weapon;
    public Armour[] equippedArmour;
    public SkinnedMeshRenderer[] equippedItemMeshes;
    public float damageReduction = 0;

    public SkinnedMeshRenderer colonistBodyMesh;

    private void Start()
    {
        equippedArmour = new Armour[Enum.GetNames(typeof(ArmourSlot)).Length - 1];
        equippedItemMeshes = new SkinnedMeshRenderer[Enum.GetNames(typeof(ArmourSlot)).Length - 1];
        colonistBodyMesh = gameObject.transform.Find("Graphics").Find("BodyMesh").GetComponent<SkinnedMeshRenderer>();
    }

    public void UpdateDamageResistance(Armour oldArmour, Armour newArmour)
    {
        //regardless of if we have a new armour piece we need to be reducing the DR of the old piece
        damageReduction -= oldArmour.DamageReduction;
        //then if we have new armour to swap out
        if (newArmour != null)
        {
            //we increase the DR again
            damageReduction += newArmour.DamageReduction;
        }
        Mathf.Clamp(damageReduction, 0, 100);
    }
    //inspired by https://www.youtube.com/watch?v=ZBLvKR2E62Q&t=401s
    public void EquipWearable(Wearable wearable)
    {
        int slotIndex = (int)wearable.armourSlot;
        if (slotIndex != (int)ArmourSlot.Weapon)
        {
            //check to see if the colonist is wearing armour
            if (equippedArmour[slotIndex] != null)
            {
                //if so get the new DR
                UpdateDamageResistance(equippedArmour[slotIndex], wearable as Armour);
            }
            //then just make the corresponding slot contain the new armour info
            equippedArmour[slotIndex] = Instantiate(wearable) as Armour;
            SkinnedMeshRenderer newMesh = Instantiate(wearable.equippableMesh);
            newMesh.transform.parent = colonistBodyMesh.transform;
            newMesh.bones = colonistBodyMesh.bones;
            newMesh.rootBone = colonistBodyMesh.rootBone;
            equippedItemMeshes[slotIndex] = newMesh;


        }
        else
        {
            weapon = wearable as Weapon;
        }
    }
    public void UnequipArmour(ArmourSlot slot)
    {
        int slotIndex = (int)slot;
        //if there is no armour, we shouldnt be unequipping anyway
        if (equippedArmour[slotIndex] != null)
        {
            if (equippedItemMeshes[slotIndex] != null)
            {
                Destroy(equippedItemMeshes[slotIndex].gameObject);
            }
            UpdateDamageResistance(equippedArmour[slotIndex], null);
            equippedArmour[slotIndex] = null;
        }
    }
}
