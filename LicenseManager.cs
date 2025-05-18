using UnityEngine;

public class LicenseManager : MonoBehaviour
{
    public static LicenseManager Instance;
    public License selectedLicense;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SelectLicense(License license)
    {
        selectedLicense = license;
        Debug.Log("Selected license: " + license.licenseName);
        // Update your UI
    }
}

