using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    public int currentMoney = 1500;
    public TMP_Text moneyText;

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

    private void Start()
    {
        UpdateMoneyUI();
    }

    // Spend money (buying something)
    public bool TryPurchase(int itemPrice)
    {
        if (currentMoney >= itemPrice)
        {
            currentMoney -= itemPrice;
            UpdateMoneyUI();
            return true;
        }
        else
        {
            Debug.Log("Not enough money!");
            return false;
        }
    }

    // Earn money (selling something)
    public void SellItem(int sellPrice)
    {
        currentMoney += sellPrice;
        UpdateMoneyUI();
        Debug.Log($"Sold item for ${sellPrice}. Current money: ${currentMoney}");
    }

    public void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "Money: $" + currentMoney;
        }
        else
        {
            Debug.LogWarning("MoneyText reference not assigned!");
        }
    }
}


