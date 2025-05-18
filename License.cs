using UnityEngine;

[CreateAssetMenu(fileName = "NewLicense", menuName = "Crafting/License")]
public class License : InventoryItem
{   public string licenseName;
    public Sprite icon;
    public string description;
    public EssenceItem[] requiredEssences;
    
}
