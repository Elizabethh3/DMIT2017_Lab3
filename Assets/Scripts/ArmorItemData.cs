using UnityEngine;

public class ArmorItemData:InventoryItemData
{
    public int armorRating;
    public EquipmentSlot equipmentSlot;
    
    public ArmorItemData(ArmorItemSO config_)
    {
        this.config = config_;
        this.flavourText = config_.flavourText;
        this.itemName = config_.itemName;
        this.icon = config_.icon;
        this.armorRating = config_.armorRating;
        this.equipmentSlot = config_.equipmentSlot;
        quantity = 1;
    }
}
