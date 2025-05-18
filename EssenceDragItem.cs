using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EssenceDragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public EssenceItem essenceData;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    public Canvas canvas;

    void Awake()
    {   
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        canvasGroup.blocksRaycasts = true;
    }

    public void Init(EssenceItem data)
    {
        essenceData = data;
        GetComponent<Image>().sprite = data.icon;
        Debug.Log("EssenceDragItem initialized with: " + data.essenceName);

    }
}

