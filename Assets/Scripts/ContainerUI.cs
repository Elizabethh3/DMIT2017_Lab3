using System.Collections.Generic;
using UnityEngine;

public class ContainerUI : MonoBehaviour
{
    public InventoryManager targetInventory;
    public GameObject buttonPrefab;
    public Transform inventoryParent;
    public Transform containerParent;
    private List<GameObject> uiButtons = new();
    public InventoryContainer container;

    public void InitUI(InventoryContainer container_) //called immediately
    {
        Dictionary<InventoryItemSO, InventoryItemData> inventoryRef = targetInventory.inventory; //comes from inventory manager - whats in inventory
        Dictionary<InventoryItemSO, InventoryItemData> containerRef = container_.containerInventory; //whats in the container
        foreach (InventoryItemData item in inventoryRef.Values) //put items in inventory side
        {
            GameObject tmp = Instantiate(buttonPrefab, inventoryParent);
            tmp.GetComponent<ContainerButton>().InitializeButton(item, container_, false);
            uiButtons.Add(tmp);
        }

        foreach (InventoryItemData item in containerRef.Values) //put items in container side
        {
            GameObject tmp = Instantiate(buttonPrefab, containerParent);
            tmp.GetComponent<ContainerButton>().InitializeButton(item, container_, true);
            uiButtons.Add(tmp);
        }
        container_.onContainerUpdated += UpdateContainerUI;
    }

    public void UpdateContainerUI(InventoryContainer container_) //redraw inventory when changed
    {
        foreach(GameObject button in uiButtons)
        {
            Destroy(button);
        }
        uiButtons.Clear();

        Dictionary<InventoryItemSO, InventoryItemData> inventoryRef = targetInventory.inventory;
        Dictionary<InventoryItemSO, InventoryItemData> containerRef = container_.containerInventory;
        foreach (InventoryItemData item in inventoryRef.Values)
        {
            GameObject tmp = Instantiate(buttonPrefab, inventoryParent);
            tmp.GetComponent<ContainerButton>().InitializeButton(item, container_, false);
            uiButtons.Add(tmp);
        }

        foreach (InventoryItemData item in containerRef.Values)
        {
            GameObject tmp = Instantiate(buttonPrefab, containerParent);
            tmp.GetComponent<ContainerButton>().InitializeButton(item, container_, true);
            uiButtons.Add(tmp);
        }

    }

    public void UnDrawUI()
    {
        foreach(GameObject button in uiButtons)
        {
            Destroy(button);
        }
        uiButtons.Clear();
    }
}
