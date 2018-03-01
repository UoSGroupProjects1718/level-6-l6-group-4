using UnityEngine;
using System;


public class Equipment : MonoBehaviour
{
    public Weapon weapon;
    public Armour[] equippedArmour;
    public MeshFilter[] equippedItemFilters;
    public MeshRenderer[] equippedItemRenderers;


    public float damageReduction = 0;


    private void Awake()
    {

        equippedArmour = new Armour[Enum.GetNames(typeof(ArmourSlot)).Length - 1];
        //setup the item mesh filter references
        equippedItemFilters = new MeshFilter[Enum.GetNames(typeof(ArmourSlot)).Length];
        equippedItemFilters[(int)ArmourSlot.Head] = transform.Find("Graphics").Find("HeadSlot").GetComponent<MeshFilter>();
        equippedItemFilters[(int)ArmourSlot.Torso] = transform.Find("Graphics").Find("TorsoSlot").GetComponent<MeshFilter>();
        equippedItemFilters[(int)ArmourSlot.Legs] = transform.Find("Graphics").Find("LegSlot").GetComponent<MeshFilter>();
        equippedItemFilters[(int)ArmourSlot.Weapon] = transform.Find("Graphics").Find("WeaponSlot").GetComponent<MeshFilter>();
        //set the mesh renderers
        equippedItemRenderers = new MeshRenderer[Enum.GetNames(typeof(ArmourSlot)).Length];
        equippedItemRenderers[(int)ArmourSlot.Head] = transform.Find("Graphics").Find("HeadSlot").GetComponent<MeshRenderer>();
        equippedItemRenderers[(int)ArmourSlot.Torso] = transform.Find("Graphics").Find("TorsoSlot").GetComponent<MeshRenderer>();
        equippedItemRenderers[(int)ArmourSlot.Legs] = transform.Find("Graphics").Find("LegSlot").GetComponent<MeshRenderer>();
        equippedItemRenderers[(int)ArmourSlot.Weapon] = transform.Find("Graphics").Find("WeaponSlot").GetComponent<MeshRenderer>();

        //colonistBodyMesh = gameObject.transform.Find("Graphics").Find("BodyMesh").GetComponent<SkinnedMeshRenderer>();
    }

    public void UpdateDamageResistance(Armour oldArmour, Armour newArmour)
    {

        if(oldArmour != null)
        {
            //regardless of if we have a new armour piece we need to be reducing the DR of the old piece
            damageReduction -= oldArmour.DamageReduction;
            //update the walk speed modifier.
            GetComponent<ColonistController>().UpdateMoveSpeed(-oldArmour.walkSpeedModifier);
        }

        //then if we have new armour to swap out
        if (newArmour != null)
        {
            //we increase the DR
            damageReduction += newArmour.DamageReduction;
            //update the walk speed modifier
            GetComponent<ColonistController>().UpdateMoveSpeed(newArmour.walkSpeedModifier);
        }
        damageReduction = Mathf.Clamp(damageReduction, 0, 100);

    }

    //inspired by https://www.youtube.com/watch?v=ZBLvKR2E62Q&t=401s
    public void EquipWearable(Wearable wearable)
    {
        int slotIndex = (int)wearable.armourSlot;
        if (slotIndex != (int)ArmourSlot.Weapon)
        {

                //update damage resistance
                UpdateDamageResistance(equippedArmour[slotIndex], wearable as Armour);
            
            //then make the corresponding slot contain the new armour info
            equippedArmour[slotIndex] = wearable as Armour;
            //now assign the correct graphic to the correct slot
            equippedItemFilters[slotIndex].mesh = wearable.itemMesh;
            equippedItemRenderers[slotIndex].materials = wearable.itemRenderer.sharedMaterials;


            //update the colonist information panel
            if (UIController.Instance.focusedColonist == GetComponent<ColonistController>())
            {
                UIController.Instance.UpdateColonistInfoPanel(UIController.Instance.focusedColonist);
            }

        }
        else
        {
            weapon = wearable as Weapon;
            equippedItemFilters[slotIndex].mesh = wearable.itemMesh;
            equippedItemRenderers[slotIndex].materials = wearable.itemRenderer.sharedMaterials;
        }
    }
    public void UnequipArmour(ArmourSlot slot)
    {
        int slotIndex = (int)slot;
        if(slot == ArmourSlot.Weapon)
        {
            //weapon's skinnedmeshrenderer to disabled

            weapon = null;
            equippedItemFilters[slotIndex].mesh = null;
            return;
        }
        //if there is no armour, we shouldnt be unequipping anyway
        if (equippedArmour[slotIndex] != null)
        {
            //update the damage resistance
            UpdateDamageResistance(equippedArmour[slotIndex], null);
            //nullify the armour variables
            equippedArmour[slotIndex] = null;
            equippedItemFilters[slotIndex].mesh = null;
        }
    }

}
