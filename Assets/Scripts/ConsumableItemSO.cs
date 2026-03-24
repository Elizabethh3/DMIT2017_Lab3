using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableItemSO", menuName = "Inventory System/ConsumableItemSO")]
public class ConsumableItemSO : InventoryItemSO
{
    public override InventoryItemData CreateRuntimeData()
    {
        return new ConsumableItemData(this);
    }
}
