using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ConversationManager : MonoBehaviour
{
    private List<InventoryItem> allItems;               
    public InventoryManager inventoryManager;         
    public MoneyManager moneyManager;                 
    public ExperienceManager experienceManager;       

    public GameObject twoOptionPanel;                 
    public GameObject threeOptionPanel;               

    public Button optionA, optionB, optionC;          
    public Button haveItButton, dontHaveButton;      
    public Text npcText1;
    public Text npcText2;

    private InventoryItem currentItem;

    public event System.Action OnConversationEnded;
    
    void Awake()
    {
       allItems = new List<InventoryItem>();
       License[] licenses = Resources.LoadAll<License>("Licenses");
    foreach (var license in licenses)
    {
        allItems.Add(license);
    }

    
    }
    public void StartConversation(NPCController npc)
    {
        if (allItems.Count == 0)
    {
        Debug.LogError("No items loaded! Check Resources folder or item setup.");
        return;
    }
        
        currentItem = allItems[Random.Range(0, allItems.Count)];
        bool isNameCase = Random.value > 0.5f;

        if (isNameCase)
        {
            npcText1.text = $"Hello! Do you have {currentItem.itemName} perfume?";
            SetupTwoOptionButtons();
        }
        else
        {
            npcText2.text = $"Hi! I'm looking for perfume that has {currentItem.itemDescription} notes.. Can you recommend something?";
            SetupThreeOptionButtons();
        }
    }

    void SetupTwoOptionButtons()
    {
        twoOptionPanel.SetActive(true);
        threeOptionPanel.SetActive(false);

        haveItButton.onClick.RemoveAllListeners();
        dontHaveButton.onClick.RemoveAllListeners();

        haveItButton.onClick.AddListener(() => HandleHaveIt());
        dontHaveButton.onClick.AddListener(() => HandleDontHaveIt());
    }

    void SetupThreeOptionButtons()
    {
        twoOptionPanel.SetActive(false);
        threeOptionPanel.SetActive(true);

        
        List<InventoryItem> shuffledOptions = new List<InventoryItem>(allItems);
        shuffledOptions.Remove(currentItem);
        ShuffleList(shuffledOptions);

        InventoryItem wrong1 = shuffledOptions[0];
        InventoryItem wrong2 = shuffledOptions[1];

        List<(Button, InventoryItem)> buttonPairs = new List<(Button, InventoryItem)>
        {
            (optionA, currentItem),
            (optionB, wrong1),
            (optionC, wrong2)
        };
        ShuffleButtonPairs(buttonPairs);

        foreach (var pair in buttonPairs)
        {
            pair.Item1.GetComponentInChildren<TMP_Text>().text = pair.Item2.itemName;
            pair.Item1.onClick.RemoveAllListeners();
            pair.Item1.onClick.AddListener(() => HandleThreeOptionChoice(pair.Item2));
        }
    }

    void HandleHaveIt()
    {
        if (inventoryManager.HasItem(currentItem))
        {
            inventoryManager.RemoveItem(currentItem);
            moneyManager.SellItem(currentItem.sellPrice);
            experienceManager.GainXP(10);  
            ShowXPResultPopup(true);
        }
        else
        {
            experienceManager.LoseXP(5); 
            ShowXPResultPopup(false);
        }
        EndConversation();
    }

    void HandleDontHaveIt()
    {
        experienceManager.LoseXP(5);  
        ShowXPResultPopup(false);
        EndConversation();
    }

    void HandleThreeOptionChoice(InventoryItem chosen)
    {
       
        inventoryManager.RemoveItem(chosen);
        moneyManager.SellItem(chosen.sellPrice);

        if (chosen == currentItem)
        {
            experienceManager.GainXP(10);
            ShowXPResultPopup(true);
        }
        else
        {
            experienceManager.LoseXP(5);
            ShowXPResultPopup(false);
        }
        EndConversation();
    }

    void EndConversation()
    {
        twoOptionPanel.SetActive(false);
        threeOptionPanel.SetActive(false);
        Debug.Log("Conversation ended.");
        OnConversationEnded?.Invoke();
    }

    void ShowXPResultPopup(bool gained)
    {
        int amount = gained ? 10 : -5;
        Debug.Log(gained ? $"+{amount} XP" : $"{amount} XP");
        
    }

    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int rand = Random.Range(i, list.Count);
            list[i] = list[rand];
            list[rand] = temp;
        }
    }

    void ShuffleButtonPairs(List<(Button, InventoryItem)> pairs)
    {
        for (int i = 0; i < pairs.Count; i++)
        {
            var temp = pairs[i];
            int rand = Random.Range(i, pairs.Count);
            pairs[i] = pairs[rand];
            pairs[rand] = temp;
        }
    }
}



