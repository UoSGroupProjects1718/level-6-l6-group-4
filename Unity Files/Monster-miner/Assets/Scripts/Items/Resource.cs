using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/ Resource")]

public class Resource : ItemInfo
{
    public int GatherWorkPerItem;
    [Range(0, 1000)]
    public int minDropAmount;
    [Range(0, 1000)]
    public int maxDropAmount;
}
