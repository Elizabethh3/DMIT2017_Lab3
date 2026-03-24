using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryContainer : MonoBehaviour
{
    public Dictionary<InventoryItemSO, InventoryItemData> containerInventory = new();
    public List<InventoryItemSO> startingInventory = new();
    public InventoryManager playerInventoryManager;
    public event Action<InventoryContainer> onContainerUpdated;
    public bool empty;

    void Awake()
    {
        playerInventoryManager = FindAnyObjectByType<InventoryManager>();
    }
    private void Start()
    {
        empty = false;
        foreach(InventoryItemSO item in startingInventory)
        {
            if (!containerInventory.TryAdd(item, item.CreateRuntimeData()))
            {
                containerInventory[item].quantity++;

            }
        }

    }

    public void AddItemToContainer(InventoryItemSO itemToAdd_) // call this when you pick up an item
    {
        playerInventoryManager.RemoveItem(itemToAdd_);
        if (!containerInventory.TryAdd(itemToAdd_, itemToAdd_.CreateRuntimeData()))
        {
            containerInventory[itemToAdd_].quantity++;

        }
        onContainerUpdated?.Invoke(this);
       // onInventoryUpdate?.Invoke(inventory);
    }

    public void AddItemToPlayerInventory(InventoryItemSO itemToAdd_) // call this when you lose an item
    {
        if (containerInventory.TryGetValue(itemToAdd_, out InventoryItemData data))
        {
            if (containerInventory[itemToAdd_].quantity > 1)
            {
                containerInventory[itemToAdd_].quantity--;

            }
            else containerInventory.Remove(itemToAdd_);
        }
        playerInventoryManager.AddItem(itemToAdd_);
        onContainerUpdated?.Invoke(this);
        if (containerInventory.Count == 0)
        {
            empty = true;
        }
        else
        {
            empty = false;
        }
       // onInventoryUpdate?.Invoke(inventory);
    }
}
