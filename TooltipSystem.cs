using UnityEngine;
using UnityEngine.UI;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem Instance;
    public GameObject tooltipPanel;
    public Text tooltipText;
    public Vector2 offset = new Vector2(100f, -100f);

    private RectTransform tooltipRect;

    void Awake()
    {
        Instance = this;
        tooltipRect = tooltipPanel.GetComponent<RectTransform>();
        Hide();
    }

    void Update()
    {
        if (tooltipPanel.activeSelf)
        {
            Vector2 pos = Input.mousePosition + (Vector3)offset;
            tooltipRect.position = pos;
        }
    }

    public static void Show(string content)
    {
        Instance.tooltipPanel.SetActive(true);
        Instance.tooltipText.text = content;
    }

    public static void Hide()
    {
        Instance.tooltipPanel.SetActive(false);
    }
}


