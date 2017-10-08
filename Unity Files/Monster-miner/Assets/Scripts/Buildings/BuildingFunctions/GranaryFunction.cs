using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranaryFunction : BuildingFunction {

    [SerializeField]
    private GameObject itemPrefab;

    [SerializeField]
    private List<Nutrition> inventory;
    [SerializeField]
    private int inventorySpace;


    public void Awake()
    {
        inventory = new List<Nutrition>();
    }
    public override void Function()
    {
        //show ui elements on click
    }

    public void AddItem(Nutrition newItem)
    {
        //loop through each item in the list to see if we can place our new item's stack within another item's stack
        foreach(Nutrition item in inventory)
        {
            if(item.itemName == newItem.itemName && item.currentStackAmount != item.maxStackAmount)
            {
                if(item.currentStackAmount + newItem.currentStackAmount <= item.maxStackAmount)
                {
                    item.currentStackAmount += newItem.currentStackAmount;
                    return;
                }
            }
        }
        //if we cant find a place for the item in the first object we look into, we will just add a new entry to the inventory
        if(inventory.Count < inventorySpace)
        {
            inventory.Add(newItem);
        }
    }

    public GameObject RemoveItem(Nutrition item, int amount)
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].itemName == item.itemName)
            {
                //if the items current stack - amount does not go below 0 and equals 0
                if(inventory[i].currentStackAmount - amount == 0 && inventory[i].currentStackAmount - amount > -1)
                {
                    Nutrition  obj = Instantiate(inventory[i]);
                    obj.currentStackAmount = amount;
                    inventory.Remove(inventory[i]);
                    Item newItem = Instantiate(itemPrefab).GetComponent<Item>();
                    newItem.item = obj;
                    newItem.gameObject.name = newItem.item.itemName;
                    return newItem.gameObject;

                }
                else
                {
                    int difference = amount - inventory[i].currentStackAmount;
                    Item newItem = Instantiate(itemPrefab).GetComponent<Item>();
                    newItem.item = Instantiate(inventory[i]);
                    newItem.item.currentStackAmount = difference;
                    newItem.gameObject.name = newItem.item.itemName;
                    inventory.Remove(inventory[i]);
                    return newItem.gameObject;
                }
            }
        }
        return null;
    }

    



}
