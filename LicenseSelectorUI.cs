using UnityEngine;
using UnityEngine.UI;

public class LicenseSelectorUI : MonoBehaviour
{
    public Transform licenseContainer;
    public GameObject licenseButtonPrefab;

    public License[] availableLicenses;

    void Start()
    {
        foreach (License license in availableLicenses)
        {
            GameObject btn = Instantiate(licenseButtonPrefab, licenseContainer);
            
            // Set icon
            btn.transform.Find("Image").GetComponent<Image>().sprite = license.icon;
            
            // Set name
            btn.GetComponentInChildren<Text>().text = license.licenseName;

            // Tooltip logic
            TooltipTrigger tooltip = btn.GetComponent<TooltipTrigger>();
            tooltip.tooltipText = GenerateTooltipText(license);

            // Button logic
            btn.GetComponent<Button>().onClick.AddListener(() => {
                LicenseManager.Instance.SelectLicense(license);
            });
        }
    }

    string GenerateTooltipText(License license)
    {
        string tooltip = license.description + "\n\nRequired Essences:\n";
        foreach (var essence in license.requiredEssences)
        {
            tooltip += "• " + essence.essenceName + "\n";
        }
        return tooltip;
    }
}

