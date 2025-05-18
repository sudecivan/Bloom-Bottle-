using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EssencePanelUI : MonoBehaviour
{
    [Header("Prefab & Container")]
    public GameObject essencePrefab; 
    public Transform container;      

    [Header("Essences to Display")]
    public List<EssenceItem> availableEssences = new List<EssenceItem>();

    void Start()
    {
        if (essencePrefab == null || container == null)
        {
            Debug.LogError("EssencePanelUI is missing prefab or container references.");
            return;
        }

        PopulateEssencePanel();
    }

    public void PopulateEssencePanel()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

       
        foreach (EssenceItem essence in availableEssences)
        {
            if (essence == null)
            {
                Debug.LogWarning("Null EssenceItem found in list.");
                continue;
            }

            GameObject itemGO = Instantiate(essencePrefab, container);

            EssenceDragItem dragItem = itemGO.GetComponent<EssenceDragItem>();
            if (dragItem != null)
            {
                dragItem.Init(essence);
            }
            else
            {
                Debug.LogError("EssencePrefab is missing the EssenceDragItem component!");
            }
        }
    }

    public void SetAvailableEssences(List<EssenceItem> newEssences)
    {
        availableEssences = newEssences;
        PopulateEssencePanel();
    }
}


