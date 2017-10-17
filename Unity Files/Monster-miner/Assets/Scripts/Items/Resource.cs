using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/ Resource")]

public class Resource : ItemInfo
{
    public int GatherWorkPerItem;
    public int Nutrition;
    [Range(0, 100)]
    public int minDropAmount;
    [Range(0, 100)]
    public int maxDropAmount;
}
