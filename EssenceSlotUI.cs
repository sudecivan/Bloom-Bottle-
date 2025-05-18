using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EssenceSlotUI : MonoBehaviour, IDropHandler
{
    public Canvas canvas;
    public Image slotimage;
    public EssenceItem currentEssence;

    private void Awake()
    {
            canvas = GetComponentInParent<Canvas>();
            slotimage = GetComponent<Image>();
            if (slotimage == null)
            {
                Debug.LogError("EssenceSlotUI: No Image component found or assigned.");
            }
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        EssenceDragItem draggedItem = eventData.pointerDrag.GetComponent<EssenceDragItem>();
        if (draggedItem != null && draggedItem.essenceData != null)
        {
            SetEssence(draggedItem.essenceData);
        }
        else
        {
            Debug.LogWarning("Dropped item missing EssenceDragItem or essenceData.");
        }
    }

    public void SetEssence(EssenceItem essence)
    {
        if (essence == null)
        {
            Debug.LogError("SetEssence called with null essence.");
            return;
        }

        if (essence.icon == null)
        {
            Debug.LogError($"Essence '{essence.essenceName}' is missing an icon.");
            return;
        }

        if (slotimage == null)
        {
            Debug.LogError("Icon is not assigned in EssenceSlotUI.");
            return;
        }

        currentEssence = essence;
        slotimage.sprite = essence.icon;
        slotimage.enabled = true;

        Debug.Log($"Essence set: {essence.essenceName}");
    }

    public void ClearSlot()
    {
        currentEssence = null;
        if (slotimage != null)
        {
            slotimage.sprite = null;
            slotimage.enabled = false;
        }
    }
}



