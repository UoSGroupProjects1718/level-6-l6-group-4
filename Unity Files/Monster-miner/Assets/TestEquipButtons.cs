using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEquipButtons : MonoBehaviour {

    public Armour head;
    public Armour torso;
    public Armour legs;

    public ColonistController colonist;

    public void HeadClick()
    {
        try
        {
            colonist.colonistEquipment.EquipWearable(ItemDatabase.GetItem("Hunter Head") as Wearable);
            Debug.Log("Head button pressed");
        }
        catch
        {
            Debug.LogWarning("Could not get Hunter Head");
        }
    }
    public void TorsoClick()
    {
        try
        {
            colonist.colonistEquipment.EquipWearable(ItemDatabase.GetItem("Hunter Chest") as Wearable);

        }
        catch
        {
            Debug.LogWarning("Could not get Hunter Chest");
        }
    }
    public void LegsClick()
    {
        try
        {

        colonist.colonistEquipment.EquipWearable(ItemDatabase.GetItem("Hunter Legs") as Wearable);
        }
        catch
        {
            Debug.LogWarning("Could not get Hunter Legs");
        }
    }
}
