using UnityEngine;

[CreateAssetMenu(fileName = "WeaponItemSO", menuName = "Inventory System/WeaponItemSO")]
public class WeaponItemSO : InventoryItemSO
{
    public int weaponStrength;
    public int weaponDurability;

    public override InventoryItemData CreateRuntimeData()
    {
        return new WeaponItemData(this);
    }
}
