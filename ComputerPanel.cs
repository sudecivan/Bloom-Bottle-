using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ComputerPanel : MonoBehaviour, IInteractable
{ public GameObject computerInterface;
 
    public string GetDescription()
    {
        return "press key to interact";
    }

    public void Interact()
    {
        computerInterface.SetActive(true);
        

    }
    
    
}

    
