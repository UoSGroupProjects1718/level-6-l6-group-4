using UnityEngine;
using System;


public class Equipment : MonoBehaviour
{
    public Weapon weapon;
    public Armour[] equippedArmour;
    public MeshFilter[] equippedItemFilters;
    public MeshRenderer[] equippedItemRenderers;


    public float damageReduction = 0;

    //public SkinnedMeshRenderer colonistBodyMesh;

    private Transform headTransform;
    private Transform torsoTransform;
    private Transform legTransform;

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
           
            
            
            //SkinnedMeshRenderer newMesh = ItemDatabase.GetItemSkinnedMeshRenderer(wearable.itemName);
            //newMesh.transform.parent = colonistBodyMesh.transform;
            //newMesh.bones = colonistBodyMesh.bones;
            //newMesh.rootBone = colonistBodyMesh.rootBone;
            //equippedItemMeshes[slotIndex] = newMesh;


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
