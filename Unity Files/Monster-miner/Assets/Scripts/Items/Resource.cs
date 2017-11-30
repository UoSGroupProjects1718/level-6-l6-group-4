using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/ Resource")]

public class Resource : ItemInfo
{
    [Range(0, 1000)]
    public int minDropAmount;
    [Range(0, 1000)]
    public int maxDropAmount;
}
