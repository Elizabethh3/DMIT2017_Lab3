using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public InventoryManager targetInventory;
    public GameObject buttonPrefab;
    public Transform contentParent;
    private List<GameObject> uiButtons = new();

    private void Start()
    {
        targetInventory.onInventoryUpdate += UpdateUI;
    }
    [ContextMenu("Init UI")]
    public void InitUI()
    {
        Dictionary<InventoryItemSO, InventoryItemData> inventoryRef = targetInventory.inventory;

        foreach(InventoryItemData item in inventoryRef.Values)
        {
            GameObject tmp = Instantiate(buttonPrefab, contentParent);
            tmp.GetComponent<InventoryButton>().InitializeButton(item);
            uiButtons.Add(tmp);
        }
    }

    public void UpdateUI(Dictionary<InventoryItemSO, InventoryItemData> inventory_)
    {
        foreach(GameObject go in uiButtons)
        {
            Destroy(go);
        }
        uiButtons.Clear();

        foreach (InventoryItemData item in inventory_.Values)
        {
            GameObject tmp = Instantiate(buttonPrefab, contentParent);
            tmp.GetComponent<InventoryButton>().InitializeButton(item);
            uiButtons.Add(tmp);
        }
    }
}
