using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContainerButton : MonoBehaviour
{
    public TMP_Text itemName;
    public TMP_Text flavourText;
    public Image icon;
    public TMP_Text quantityDisplay;
    private InventoryItemData itemData;
    private InventoryContainer inventoryContainer;
    private bool isContainerButton;

    public void InitializeButton(InventoryItemData itemData_, InventoryContainer container_, bool isContainerButton_)
    {
        itemData = itemData_;
        isContainerButton = isContainerButton_;
        inventoryContainer = container_;

        itemName.text = itemData.itemName;
        flavourText.text = itemData.flavourText;
        quantityDisplay.text = itemData.quantity.ToString();
        icon.sprite = itemData.icon;

        GetComponent<Button>().onClick.AddListener(ButtonClick);
    }

    public void ButtonClick()
    {
        // InventoryContainer container;
        // container.AddItemToContainer(itemData.config);
        // container.AddItemToPlayerInventory(itemData.config);
        if (isContainerButton)
        {
            inventoryContainer.AddItemToPlayerInventory(itemData.config);
            return;
        }
        inventoryContainer.AddItemToContainer(itemData.config);
    }
}
