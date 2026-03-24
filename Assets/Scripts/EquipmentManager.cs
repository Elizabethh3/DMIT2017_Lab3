using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Dictionary<EquipmentSlot, InventoryItemData> equipmentDictionary = new();
    public static EquipmentManager instance;
    public InventoryManager inventory;
    public event Action<Dictionary<EquipmentSlot, InventoryItemData>> onEquip; // event to trigger when items are equipped ; updates ui

    private void Awake()
    {
        if (instance == null) instance = this;
        InitializeEquipment();
    }

    public void InitializeEquipment()
    {
        // initialize equipment as empty to start
        equipmentDictionary.Add(EquipmentSlot.HELM, null);
        equipmentDictionary.Add(EquipmentSlot.CHEST, null);
        equipmentDictionary.Add(EquipmentSlot.ARMS, null);
        equipmentDictionary.Add(EquipmentSlot.BOOTS, null);
        equipmentDictionary.Add(EquipmentSlot.WEAPON, null);

    }

    public void EquipItem(InventoryItemData itemToEquip)
    {
        // equip armor or weapon to corresponding slot
        if(itemToEquip is ArmorItemData armor)
        {
            if (equipmentDictionary[armor.equipmentSlot] != null) inventory.AddItem(equipmentDictionary[armor.equipmentSlot].config); // if there is already something equipped, remove it and send it back to inventory
            equipmentDictionary[armor.equipmentSlot] = armor;
            inventory.RemoveItem(itemToEquip.config); // after equipping remove item from inventory
            Debug.Log(equipmentDictionary[armor.equipmentSlot].itemName + " was equipped");
        }
        // same logic but for weapons
        else if (itemToEquip is WeaponItemData weapon) 
        {
            if (equipmentDictionary[EquipmentSlot.WEAPON] != null) inventory.AddItem(equipmentDictionary[EquipmentSlot.WEAPON].config);

            equipmentDictionary[EquipmentSlot.WEAPON] = weapon;
            inventory.RemoveItem(itemToEquip.config);

            Debug.Log(equipmentDictionary[EquipmentSlot.WEAPON].itemName + " was equipped");
        }
        onEquip?.Invoke(equipmentDictionary);// event trigger to update ui etc
    }

    
}
