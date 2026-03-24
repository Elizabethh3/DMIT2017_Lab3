using UnityEngine;

public class ConsumableItemData:InventoryItemData
{
    public ConsumableItemData(ConsumableItemSO config)
    {
        this.config = config; 
        this.flavourText = config.flavourText;
        this.itemName = config.itemName;
        this.icon = config.icon;
        this.quantity = 1;
    }
}
