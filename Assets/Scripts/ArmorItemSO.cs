using UnityEngine;

[CreateAssetMenu(fileName = "ArmorItemSO", menuName = "Inventory System/ArmorItemSO")]
public class ArmorItemSO : InventoryItemSO
{
    public int armorRating;
    public EquipmentSlot equipmentSlot;

    public override InventoryItemData CreateRuntimeData()
    {
        return new ArmorItemData(this);
    }
}

public enum EquipmentSlot
{
    HELM,
    CHEST,
    ARMS,
    BOOTS,
    WEAPON
}
