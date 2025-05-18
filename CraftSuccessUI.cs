using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingSuccessUI : MonoBehaviour
{
    public static CraftingSuccessUI Instance;

    public GameObject panel;
    public Image icon;
    public TextMeshProUGUI messageText;
    public Animator sparkleAnimator;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void ShowSuccess(License craftedLicense)
    {
        icon.sprite = craftedLicense.icon;
        messageText.text = $"Good job! You crafted {craftedLicense.licenseName}!";
        panel.SetActive(true);

        if (sparkleAnimator != null)
            sparkleAnimator.SetTrigger("Play");
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    public void OnCloseButton(){
    CraftingSuccessUI.Instance.Hide();
}
}

