using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance;

    [Header("XP Stats")]
    public int currentXP = 0;
    public int currentLevel = 1;
    public int xpToNextLevel = 100;

    [Header("UI")]
    public Slider xpBar;
    public TMP_Text xpText;
    public TMP_Text levelText;
    public GameObject xpPopupPrefab; 
    public Transform popupParent;    

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
        UpdateXPUI();
    }

    public void GainXP(int amount)
    {
        currentXP += amount;
        ShowXPPopup($"+{amount} XP", Color.green);

        CheckLevelUp();
        UpdateXPUI();
    }

    public void LoseXP(int amount)
    {
        currentXP -= amount;
        if (currentXP < 0) currentXP = 0;

        ShowXPPopup($"-{amount} XP", Color.red);
        UpdateXPUI();
    }

    private void CheckLevelUp()
    {
        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            currentLevel++;
            xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.2f); 

            Debug.Log($"Level Up! You are now level {currentLevel}");
        }
    }

    private void UpdateXPUI()
    {
        if (xpBar != null)
            xpBar.value = (float)currentXP / xpToNextLevel;

        if (xpText != null)
            xpText.text = $"{currentXP}/{xpToNextLevel} XP";

        if (levelText != null)
            levelText.text = $"Level {currentLevel}";
    }

    private void ShowXPPopup(string text, Color color)
    {  Debug.Log($"Trying to show popup: {text}");
        if (xpPopupPrefab != null && popupParent != null)
        {
            GameObject popup = Instantiate(xpPopupPrefab, popupParent);
            TMP_Text popupText = popup.GetComponentInChildren<TMP_Text>();
            popupText.text = text;
            popupText.color = color;

            Destroy(popup, 2f); 
        }
    }
}

