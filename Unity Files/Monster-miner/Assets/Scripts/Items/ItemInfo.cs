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
[CreateAssetMenu(menuName = "Items/ Item")]
public class ItemInfo : ScriptableObject {

    public string itemName;
    [TextArea]
    public string itemDescription;
    public float decaySpeed;
    [Range(0,200)]
    public float maxItemDurability;
    [HideInInspector]
    public float currentItemDurability;
    public int maxStackAmount;
    public int currentStackAmount;
    public Mesh itemMesh;
    public ItemType type;

    public float nutrition;
    [Range(0, 100)]
    public int minDropAmount;
    [Range(0, 100)]
    public int maxDropAmount;
    private void Awake()
    {
        currentItemDurability = maxItemDurability;
    }

}
