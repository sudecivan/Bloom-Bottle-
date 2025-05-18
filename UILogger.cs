using UnityEngine;
using TMPro;
using System.Collections;

public class UILogger : MonoBehaviour
{
    public static UILogger Instance;

    public TMP_Text logText;
    private Coroutine clearRoutine;

    private void Awake()
    {
        Instance = this;
    }

    public void Log(string message)
    {
        if (logText != null)
        {
            logText.text = message;

            // If a previous coroutine is running, stop it
            if (clearRoutine != null)
                StopCoroutine(clearRoutine);

            clearRoutine = StartCoroutine(ClearAfterSeconds(3f));
        }
    }

    private IEnumerator ClearAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        logText.text = "";
        clearRoutine = null;
    }
}


