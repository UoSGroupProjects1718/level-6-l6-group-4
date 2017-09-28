using UnityEngine;

public class StockpileFunction : InventoryBuilding {

    /// <summary>
    /// The standard function to add an item to the stockpile
    /// Defaults to adding one item to the pile
    /// </summary>
    /// <param name="rawMaterial">An instance of the RawMaterial class</param>
    public void AddItem(RawMaterial rawMaterial)
    {
        //loop through the inventory 
        for(int i = 0; i < inventorySize; i++)
        {
            //if there is an instance of the object we are adding, and it does not exceed the stack limit for that type of object
            if(inventory[i] != null && inventory[i].materialType == rawMaterial.materialType && inventory[i].currentStackSize < inventory[i].maxStackSize)
            {
                //increase the current stack size
                inventory[i].currentStackSize++;
                //and return
                break;
            }
            //if the previous is not true, then we check for if the current slot is null
            if(inventory[i] == null)
            {
                //if it is then we can assign a new slot in the inventory
                inventory[i] = rawMaterial;
                inventory[i].currentStackSize = 1;
                //then we can return 
                break;
            }
            //if we have reached the end of the inventory and it is not null
            if(i == inventorySize-1 && inventory[i] != null)
            {
                //then we have failed in our task
                break;
            }
        }
    }
    /// <summary>
    /// The standard function to add an item to the stockpile
    ///  The amount to add is specified as an overload
    /// </summary>
    /// <param name="rawMaterial">An instance of the RawMaterial class</param>
    /// <param name="amountToAdd">The amount of the item to add to the stockpile</param>
    public void AddItem(RawMaterial rawMaterial, int amountToAdd)
    {
        //loop through the inventory 
        for (int i = 0; i < inventorySize; i++)
        {
            //if the inventory slot is empty, then we check for if the current slot is null
            if (inventory[i] == null)
            {
                //if it is then we can assign a new slot in the inventory
                inventory[i] = rawMaterial;
                inventory[i].currentStackSize = amountToAdd;
                //then we can return 
                return;
            }

            //if there is an instance of the object we are adding, and it does not exceed the stack limit for that type of object
            if (inventory[i] != null && inventory[i].materialType == rawMaterial.materialType)
            {
                if (inventory[i].currentStackSize + amountToAdd < inventory[i].maxStackSize)
                {
                    //increase the current stack size
                    inventory[i].currentStackSize+= amountToAdd;
                    //and return
                    return;
                }

                int difference = inventory[i].maxStackSize - inventory[i].currentStackSize;

                if(difference != 0 && inventory[i].currentStackSize + difference <= inventory[i].maxStackSize)
                {
                   
                    int differentAmountToAdd = amountToAdd - difference;

                    inventory[i].currentStackSize += difference;
                    AddItem(rawMaterial, differentAmountToAdd);
                    return;

                }
            }
            //if we have reached the end of the inventory and it is not null
            if (i == inventorySize - 1 && inventory[i] != null)
            {
                //then we have failed in our task
                return;
            }
        }
    }
    /// <summary>
    /// The function for removing an item from the stockpile
    /// Defaults to removing one item
    /// </summary>
    /// <param name="item">The item type to be removed</param>
    /// <returns></returns>
    public bool RemoveItem(RawMaterialType item)
    {
        for(int i = 0; i < inventorySize; i++)
        {
            if(inventory[i].materialType == item)
            {
                if(inventory[i].currentStackSize-1 == 0)
                {
                    inventory[i] = null;
                    return true;
                }
                if(i == inventorySize -1)
                {
                    return false;
                }
                else
                {
                    inventory[i].currentStackSize--;
                    return true;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// The function for removing an item from the stockpile
    /// The number of items to be removed is specified
    /// </summary>
    /// <param name="item">The item type to be removed</param>
    /// <param name="numberOfTimes"> The number of items that are to be removed</param>
    /// <returns></returns>
    public bool RemoveItem(RawMaterialType item, int numberOfTimes)
    {
        //loop through the inventory
        for (int i = 0; i < inventorySize; i++)
        {
            //if the inventory slot contains the right type of item
            if (inventory[i] != null && inventory[i].materialType == item)
            {
                //if the amount we wish to remove reduces the inventory size to 0
                if (inventory[i].currentStackSize - numberOfTimes == 0)
                {
                    //then set the inventory slot to null
                    inventory[i] = null;
                    return true;
                }
                //if reducing the stack size by the amount we wish to remove takes the stack size to above 0
                if(inventory[i].currentStackSize - numberOfTimes > 0)
                {
                    //then remove the required amount
                    inventory[i].currentStackSize -= numberOfTimes;
                    return true;
                }
                //figure out how many of the item can be removed without taking it below 0
                int difference = numberOfTimes - inventory[i].currentStackSize; 
                
                if (inventory[i].currentStackSize - difference == 0)
                {
                    //then set the slot to null
                    inventory[i] = null;
                    //and run the function again
                    RemoveItem(item, numberOfTimes);
                    return true;
                }
            }
            if (i == inventorySize - 1)
            {
                Debug.Log("Could not remove the required amount");
                return false;
            }

     
        }
        return false;
    }
    private void OnMouseDown()
    {
        if (built)
        {
            CanvasController.GetInstance().UITarget = gameObject;
            CanvasController.GetInstance().DisplayUI(UIType.Inventory, this);
        }
    }

}
