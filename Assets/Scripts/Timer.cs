using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class Timer : MonoBehaviour
{
    public CardZoom cardZoom; // Reference to the CardZoom component
    public TextMeshProUGUI timerText; // Reference to the TextMeshPro text
    private float timeRemaining; // To store the remaining time
    private int currentSecond = 0;  // Current second value
    private bool isTimerActive = false; // A flag to check if the timer is active

    void Start()
    {
        // Start the timer when the game begins
        ResetTimer();
    }

    void Update()
    {
        if (isTimerActive)
        {
            // Decrease the time remaining by deltaTime
            timeRemaining -= Time.deltaTime;

            // Clamp time to ensure it doesn't go below zero
            timeRemaining = Mathf.Clamp(timeRemaining, 0, cardZoom.zoomDuration);

            // Get the integer part of timeRemaining (i.e., current second)
            int newSecond = Mathf.CeilToInt(timeRemaining);

            // Only update the text if the second has changed
            if (newSecond != currentSecond)
            {
                currentSecond = newSecond;
                UpdateTimerText();
            }

            // When time reaches 0, you can add additional logic if needed
            if (timeRemaining <= 0)
            {
                // Stop the timer if time runs out
                isTimerActive = false;
            }
        }
    }

    // Reset and restart the timer when a new card is spawned
    public void ResetTimer()
    {
        // Initialize the time remaining with the zoom duration from CardZoom
        timeRemaining = cardZoom.zoomDuration;
        currentSecond = Mathf.CeilToInt(timeRemaining);
        isTimerActive = true;
        UpdateTimerText();
    }

    // Stop the timer when the correct option is selected
    public void StopTimer()
    {
        isTimerActive = false;
    }

    // Update the TextMeshPro text with the format "x detik"
    void UpdateTimerText()
    {
        timerText.text = currentSecond.ToString() + " detik";
    }
}
