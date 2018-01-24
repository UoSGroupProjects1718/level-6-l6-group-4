using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ItemType
{
    Wood,
    Stone,
    Iron,
    Bone,
    Crystal,
    //put only raw resources above this point otherwise it will break scripts
    Nutrition,
    Wearable,
}
public class ItemInfo : ScriptableObject {


    [HideInInspector]
    public GameObject attachedGameObject;

    public string itemName;
    [TextArea]
    public string itemDescription;
    public float decayPerHour;
    //maybe make this 100 on all items?
    [Range(0, 200)]
    public float maxItemDurability;
    [HideInInspector]
    public int currentStackAmount;
    public int GatherWorkPerItem;

    public Sprite uiSprite;
    public Mesh itemMesh;
    public MeshRenderer itemRenderer;
    public ItemType type;


}
