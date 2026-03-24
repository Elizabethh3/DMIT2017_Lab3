using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<InventoryItemSO, InventoryItemData> inventory = new Dictionary<InventoryItemSO, InventoryItemData>();
    public InventoryItemSO[] tmp;
    public event Action<Dictionary<InventoryItemSO, InventoryItemData>> onInventoryUpdate;

    private void OnEnable()
    {
       foreach(InventoryItemSO item in tmp)
        {
            AddItem(item);
        }

    }
    public void AddItem(InventoryItemSO itemToAdd_) // call this when you pick up an item
    {
        if(!inventory.TryAdd(itemToAdd_, itemToAdd_.CreateRuntimeData())){
            inventory[itemToAdd_].quantity++;
           
        }
        onInventoryUpdate?.Invoke(inventory);
    }

    public void RemoveItem(InventoryItemSO itemToRemove_) // call this when you lose an item
    {
        if(inventory.TryGetValue(itemToRemove_, out InventoryItemData data))
        {
            if (inventory[itemToRemove_].quantity > 1)
            {
                inventory[itemToRemove_].quantity--;

            }
            else inventory.Remove(itemToRemove_);
        }
        onInventoryUpdate?.Invoke(inventory);


    }
}
