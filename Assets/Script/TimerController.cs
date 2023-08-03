using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public float totalTime = 60f; // Total time for the timer
    private float currentTime;
    private bool isTimerRunning = false;

    public TextMeshProUGUI timerText;
    public AudioSource audioSource;
    public string timeUpText = "Time's up!";
    private bool hasPlayedAudio = false;
    private float restartDelay = 10f;

    private float timePassed; // To track the time passed since last second update
    private int previousSecond; // To store the previous second value

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;
            timePassed += Time.deltaTime;

            // Check if one second has passed
            if (timePassed >= 1f)
            {
                UpdateTimerText();
                timePassed = 0f;
            }

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                if (!hasPlayedAudio)
                {
                    PlayAudioAndDisplayText();
                    hasPlayedAudio = true;
                }
            }

            if (currentTime <= -restartDelay)
            {
                RestartLevel();
            }
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            int remainingSeconds = Mathf.CeilToInt(currentTime); // Round up the remaining seconds
            timerText.text = "Time: " + remainingSeconds.ToString();
        }
    }

    private void PlayAudioAndDisplayText()
    {
        // Play audio
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Display text
        if (timerText != null)
        {
            timerText.text = timeUpText;
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
    }

    public void ResetTimer()
    {
        currentTime = totalTime;
        timePassed = 0f;
        previousSecond = 0;
        isTimerRunning = false;
        hasPlayedAudio = false;
        UpdateTimerText();
    }

    private void RestartLevel()
    {
        // Implement the logic to reset the level here
        // For example, you can load the current scene or restart the game.
    }
}
