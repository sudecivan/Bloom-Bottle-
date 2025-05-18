using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{   public event System.Action OnInventoryChanged;
    public static InventoryManager Instance;

    public Transform inventoryPanel; 
    public GameObject inventoryItemPrefab; 

    private List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<InventoryItem, GameObject> itemUIMap = new Dictionary<InventoryItem, GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(InventoryItem itemData)
    {
        if (inventoryItemPrefab == null || inventoryPanel == null)
        {
            Debug.LogError("Inventory prefab or panel not assigned!");
            return;
        }

        if (!inventory.Contains(itemData))
        {
            inventory.Add(itemData);

            GameObject newItem = Instantiate(inventoryItemPrefab, inventoryPanel);
            newItem.transform.Find("itemName").GetComponent<TMPro.TMP_Text>().text = itemData.itemName;
            newItem.transform.Find("itemIcon").GetComponent<Image>().sprite = itemData.itemIcon;

            itemUIMap[itemData] = newItem;
            OnInventoryChanged?.Invoke();
        }
        else
        {
            Debug.LogWarning($"{itemData.itemName} already in inventory.");
        }
    }

    public bool HasItem(InventoryItem itemData)
    {
        return inventory.Contains(itemData);
    }

    public void RemoveItem(InventoryItem itemData)
    {
        if (inventory.Contains(itemData))
        {
            inventory.Remove(itemData);

            if (itemUIMap.ContainsKey(itemData))
            {
                Destroy(itemUIMap[itemData]);
                itemUIMap.Remove(itemData);
                OnInventoryChanged?.Invoke();
            }

            Debug.Log($"Removed {itemData.itemName} from inventory.");
        }
        else
        {
            Debug.LogWarning($"Tried to remove {itemData.itemName}, but it's not in inventory.");
        }
    }

    public void PrintInventory()
    {
        Debug.Log("Current Inventory:");
        foreach (var item in inventory)
        {
            Debug.Log($"- {item.itemName}");
        }
    }
    public List<License> GetLicenses()
    {
    List<License> licenses = new List<License>();
    foreach (var item in inventory)
    {
        if (item is License license)
        {
            licenses.Add(license);
        }
    }
    return licenses;
    }
    public List<InventoryItem> GetInventory()
   {
    return inventory;
   }

}


