using UnityEngine;

[CreateAssetMenu(fileName = "NewEssence", menuName = "Inventory/Essence")]
public class EssenceItem : InventoryItem
{  public string essenceName;
    public Sprite icon;
    public string description;
    public EssenceType essenceType;

    public enum EssenceType { Floral, Citrus, Woody, Fresh, Spicy }
}
    

