using UnityEngine;
using UnityEngine.UI;

public class TableInteraction : MonoBehaviour
{
    public GameObject pressETextUI;     // Assign your 'Press E' UI Text
    public Canvas craftingCanvas;       // Assign the Canvas to show on interaction

    private bool isPlayerNear = false;

    void Start()
    {
        pressETextUI.SetActive(false);
        craftingCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.A))
        {
            craftingCanvas.gameObject.SetActive(true);
            pressETextUI.SetActive(false); // Hide the prompt when interacting
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            pressETextUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            pressETextUI.SetActive(false);
        }
    }
}

