using UnityEngine;
using TMPro;

public class DescriptionPanelController : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text infoText;
    public TMP_Text priceText;

    public TMP_Text nameText;
    
    public void Show(string name, string info, int price, Vector3 position)
    {
        nameText.text = name;
        infoText.text = info;
        priceText.text = "Price: $" + price.ToString();
        Vector3 offset = new Vector3(200f, -200f, 0f); 
        panel.transform.position = position + offset;
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}

