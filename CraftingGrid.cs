using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CraftingGrid : MonoBehaviour
{  public EssenceSlotUI[] essenceSlots; 

 public void CheckCraft()
{   
   

    var selectedLicense = LicenseManager.Instance.selectedLicense;
    if (selectedLicense == null)
    {
        Debug.Log("No license selected");
        return;
    }

    EssenceItem[] required = selectedLicense.requiredEssences;
    List<EssenceItem> remainingRequired = new List<EssenceItem>(required);

    foreach (EssenceSlotUI slot in essenceSlots)
    {
        if (slot.currentEssence == null)
            continue;

        // Try to match the slot essence to any remaining required essence
        for (int i = 0; i < remainingRequired.Count; i++)
        {
            if (slot.currentEssence == remainingRequired[i])
            {
                remainingRequired.RemoveAt(i);
                break;
            }
        }
    }

    if (remainingRequired.Count == 0)
    { 
        Debug.Log("Crafting successful!");
        CraftingSuccessUI.Instance.ShowSuccess(selectedLicense);
    }
    else
        Debug.Log("Craft failed. Try again.");
}

}
