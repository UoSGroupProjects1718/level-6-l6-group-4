using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Craftable")]
public class Craftable : ScriptableObject
{
    public int workAmount;
    public ItemInfo craftedItem;
    public RequiredItem[] requiredItems;

}
