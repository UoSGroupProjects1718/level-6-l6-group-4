using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stockpile : SingletonClass<Stockpile>
{
    [SerializeField]
    private GameObject itemPrefab;

    private static List<Resource> NutritionInventory;
    [SerializeField]
    private int NutritionInventorySpace;
    [SerializeField]
    private int NutritionInventoryItemCount;

    private static List<Resource> ResourceInventory;
    [SerializeField]
    private int ResourceInventorySpace;
    [SerializeField]
    private int ResourceInventoryItemCount;

    private static List<Wearable> WearableInventory;
    [SerializeField]
    private int WearableInventorySpace;
    [SerializeField]
    private int WearableInventoryItemCount;


    public override void Awake()
    {
        base.Awake();
        NutritionInventory = new List<Resource>();
        ResourceInventory = new List<Resource>();
        WearableInventory = new List<Wearable>();
    }

    public void AddItem(Resource newItem)
    {
        //the list we wish to add to will be different based on which item type it is but the code is essentially the same
        if (newItem.type == ItemType.Nutrition)
        {
            //first we check that wecan put something in there
            if (NutritionInventoryItemCount < NutritionInventorySpace)
            {
                //then we check to see if we can put all of it in there
                if (NutritionInventoryItemCount + newItem.currentStackAmount <= NutritionInventorySpace)
                {
                    //then we see if the item is already in the stockpile and add to its current stack amount
                    foreach (Resource nutrition in NutritionInventory)
                    {
                        if (nutrition.itemName == newItem.itemName)
                        {   
                            nutrition.currentStackAmount += newItem.currentStackAmount;
                            NutritionInventoryItemCount += newItem.currentStackAmount;
                            return;
                        }
                    }
                    //otherwise just add it
                    NutritionInventoryItemCount += newItem.currentStackAmount;
                    NutritionInventory.Add(newItem);
                    return;
                }
                //if we cant put it all in then we need to find the amount we can add we change the old object's stack amount
                else
                {
                    int difference = NutritionInventoryItemCount + newItem.currentStackAmount - NutritionInventorySpace;
                    foreach (Resource nutrition in NutritionInventory)
                    {
                        if (nutrition.itemName == newItem.itemName)
                        {
                            nutrition.currentStackAmount += newItem.currentStackAmount - difference;
                            newItem.currentStackAmount = difference;
                            newItem.attachedGameObject.GetComponent<MeshRenderer>().enabled = true;
                            return;
                        }
                    }
                }
            }
        }
        else if (newItem.type == ItemType.Resource)
        {
            if (ResourceInventoryItemCount < ResourceInventorySpace)
            {
                if (ResourceInventoryItemCount + newItem.currentStackAmount <= ResourceInventorySpace)
                {
                    foreach (Resource resource in ResourceInventory)
                    {
                        if (resource.itemName == newItem.itemName)
                        {
                            resource.currentStackAmount += newItem.currentStackAmount;
                            return;
                        }
                    }
                    ResourceInventoryItemCount += newItem.currentStackAmount;
                    ResourceInventory.Add(newItem);
                    return;
                }
                else
                {
                    int difference = ResourceInventoryItemCount + newItem.currentStackAmount - ResourceInventorySpace;
                    foreach (Resource resource in ResourceInventory)
                    {
                        if (resource.itemName == newItem.itemName)
                        {
                            resource.currentStackAmount += newItem.currentStackAmount - difference;
                            newItem.currentStackAmount = difference;
                            newItem.attachedGameObject.GetComponent<MeshRenderer>().enabled = true;
                            return;
                        }
                    }
                }
            }
        }
    }
    public void AddItem(Wearable newItem)
    {
        if (WearableInventoryItemCount < WearableInventorySpace)
        {
            if (WearableInventoryItemCount + newItem.currentStackAmount <= WearableInventorySpace)
            {
                foreach (Wearable wearable in WearableInventory)
                {
                    if (wearable.itemName == newItem.itemName)
                    {
                        wearable.currentStackAmount += newItem.currentStackAmount;
                        return;
                    }
                }
                WearableInventoryItemCount += newItem.currentStackAmount;
                WearableInventory.Add(newItem);
                return;
            }
            else
            {
                int difference = ResourceInventoryItemCount + newItem.currentStackAmount - ResourceInventorySpace;
                foreach (Wearable wearable in WearableInventory)
                {
                    if (wearable.itemName == newItem.itemName)
                    {
                        wearable.currentStackAmount += newItem.currentStackAmount - difference;
                        newItem.currentStackAmount = difference;
                        newItem.attachedGameObject.GetComponent<MeshRenderer>().enabled = true;
                        return;
                    }
                }
            }
        }
    }

    //add code for removing when we get to it

}
