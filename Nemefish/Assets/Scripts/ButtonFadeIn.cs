using UnityEngine;
using UnityEngine.UI;

public class ButtonFadeIn : MonoBehaviour
{
    public CanvasGroup buttonCanvasGroup; // Assign in Inspector
    private float fadeStartTime = 3.5f;
    private float fadeEndTime = 5f;
    private bool hasFadedIn = false;

    void Start()
    {
        if (buttonCanvasGroup == null)
        {
            Debug.LogError("CanvasGroup is not assigned!");
            return;
        }

        // Start fully transparent
        buttonCanvasGroup.alpha = 0f;
        buttonCanvasGroup.interactable = false;
        buttonCanvasGroup.blocksRaycasts = false;
    }

    void Update()
    {
        float currentTime = Time.time;

        if (currentTime >= fadeStartTime && currentTime <= fadeEndTime)
        {
            float normalizedTime = (currentTime - fadeStartTime) / (fadeEndTime - fadeStartTime);
            buttonCanvasGroup.alpha = normalizedTime;
        }
        else if (currentTime > fadeEndTime && !hasFadedIn)
        {
            // Ensure final state after fade completes
            buttonCanvasGroup.alpha = 1f;
            buttonCanvasGroup.interactable = true;
            buttonCanvasGroup.blocksRaycasts = true;
            hasFadedIn = true;
        }
    }
}