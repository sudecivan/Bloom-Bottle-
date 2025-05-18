using UnityEngine;

public abstract class InventoryItem : ScriptableObject
{
    public string itemName;
    public int itemPrice;
    public int sellPrice;
    public Sprite itemIcon;
    public string itemInfo;
    public string itemDescription;
}

