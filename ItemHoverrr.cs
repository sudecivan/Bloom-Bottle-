using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemHoverr : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public InventoryItem itemData;
    public DescriptionPanelController panelController;


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (panelController != null)
        {
            panelController.Show(itemData.itemName, itemData.itemInfo, itemData.itemPrice, Input.mousePosition);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (panelController != null)
        {
            panelController.Hide();
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {   Debug.Log("Attempting purchase. Current money: " + MoneyManager.Instance.currentMoney + ", item price: " + itemData.itemPrice);
        bool bought = MoneyManager.Instance.TryPurchase(itemData.itemPrice);
        if (bought)
    {   UILogger.Instance.Log("-" + itemData.itemPrice + " $ " + "item purchased");
        InventoryManager.Instance.AddItem(itemData);
        UILogger.Instance.Log("-" + itemData.itemPrice + " $ " + "item purchased");
    }
    else
    {
        UILogger.Instance.Log("Not enough money to buy! " );
    }
    }
}

