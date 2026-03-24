using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUIController : MonoBehaviour
{
    public List<EquipmentUISlot> equipmentUISlots = new(); // list of equipment slot ui
    public Dictionary<EquipmentSlot, Image> equipmentUIDictionary = new(); // translate into dictionary for easier searching

    private void Start()
    {
        foreach(var slot in equipmentUISlots)
        {
            equipmentUIDictionary.Add(slot.equipmentType, slot.uiImage); // initialize dictionary
        }
        EquipmentManager.instance.onEquip += UpdateUI; // subscribe to equip event to update ui
    }

    public void UpdateUI(Dictionary<EquipmentSlot, InventoryItemData> equipment)
    {
        foreach(EquipmentSlot equipmentSlot in equipment.Keys) // iterate through each equipment slot
        {
            if (equipment[equipmentSlot] != null) // check if equipment is not empty
            {
                equipmentUIDictionary[equipmentSlot].sprite = equipment[equipmentSlot].icon; // set corresponding ui image to the icon from the equipped item
                Color tmp = equipmentUIDictionary[equipmentSlot].color; // reset transparency on ui
                tmp.a = 1;
                equipmentUIDictionary[equipmentSlot].color = tmp;

            }
        }
    }
}

// data structure to store ui elements with corresponding equipment slot
[Serializable]
public class EquipmentUISlot
{
    public EquipmentSlot equipmentType;
    public Image uiImage;
}
