using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public TMP_Text itemName;
    public TMP_Text flavourText;
    public Image icon;
    public TMP_Text quantityDisplay;
    private InventoryItemData itemData;

    public void InitializeButton(InventoryItemData inventoryItemData)
    {
        itemData = inventoryItemData;
        itemName.text = inventoryItemData.itemName;
        flavourText.text = inventoryItemData.flavourText;
        quantityDisplay.text = inventoryItemData.quantity.ToString();
        icon.sprite = inventoryItemData.icon;
        GetComponent<Button>().onClick.AddListener(ButtonClick);
    }

    public void ButtonClick()
    {
        EquipmentManager.instance.EquipItem(itemData);
       // InventoryContainer container;
       // container.AddItemToContainer(itemData.config);
       // container.AddItemToPlayerInventory(itemData.config);
    }
}
