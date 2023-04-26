using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GRInteraction : MonoBehaviour
{
    public float gazeDuration = 2.0f; // Duration in seconds to trigger the button

    public Image progressBar; // Progress bar UI element to display the timer progress

    public UnityEvent buttonAction; // UnityEvent to trigger the button action

    private float gazeTimer = 0.0f; // Timer to track the gaze duration
    private bool isGazing = false; // Flag to check if gazing at the button

    void Update()
    {
        // Check if the button is being gazed at
        if (isGazing)
        {
            // Update the progress bar UI element
            progressBar.fillAmount = gazeTimer / gazeDuration;

            // Increment the gaze timer
            gazeTimer += Time.deltaTime;

            // Check if the gaze duration has reached the trigger duration
            if (gazeTimer >= gazeDuration)
            {
                // Perform the button action (invoke the UnityEvent)
                buttonAction.Invoke();

                // Reset the gaze timer and flag
                gazeTimer = 0.0f;
                isGazing = false;
            }
        }
    }

    // Method to handle button gaze start event
    public void OnButtonGazeStart()
    {
        // Reset the gaze timer and flag
        gazeTimer = 0.0f;
        isGazing = true;
    }

    // Method to handle button gaze end event
    public void OnButtonGazeEnd()
    {
        // Reset the progress bar UI element
        progressBar.fillAmount = 0.0f;

        // Reset the gaze timer and flag
        gazeTimer = 0.0f;
        isGazing = false;
    }
}