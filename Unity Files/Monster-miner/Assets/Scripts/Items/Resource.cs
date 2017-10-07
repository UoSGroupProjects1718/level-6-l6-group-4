using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item/Resource")]
public class Resource : ItemInfo {

    public float nutrition;
    
    [Range(0,100)]
    public int minDropAmount;
    [Range(0,100)]
    public int maxDropAmount;


}
