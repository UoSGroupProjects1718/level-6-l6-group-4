using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Resource,
    Nutrition,
    Weapon,
    Armour,
}
public class ItemInfo : ScriptableObject {


    [HideInInspector]
    public GameObject attachedGameObject;

    public string itemName;
    [TextArea]
    public string itemDescription;
    public float decayPerHour;
    //maybe make this 100 on all items?
    [Range(0,200)]
    public float maxItemDurability;
    [HideInInspector]
    public float currentItemDurability;

    public int maxStackAmount;
    public int currentStackAmount;

    public Mesh itemMesh;
    public ItemType type;



    private void Awake()
    {
        currentItemDurability = maxItemDurability;
    }

}
