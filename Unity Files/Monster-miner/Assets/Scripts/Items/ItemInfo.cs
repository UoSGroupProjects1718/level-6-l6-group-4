using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemInfo : ScriptableObject {

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

    private void Awake()
    {
        currentItemDurability = maxItemDurability;
    }

}
