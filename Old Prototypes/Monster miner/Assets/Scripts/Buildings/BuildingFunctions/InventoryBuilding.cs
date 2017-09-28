using UnityEngine;

public class InventoryBuilding : PlaceableBuilding
{
    public RawMaterial[] inventory;

    public int inventorySize;

    void Start()
    {
        inventory = new RawMaterial[inventorySize];

    }	
}
