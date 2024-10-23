using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public CardZoom cardZoom; // Reference to the CardZoom component
    public TextMeshProUGUI timerText; // Reference to the TextMeshPro text
    private float timeRemaining; // To store the remaining time
    private int currentSecond = 0;  // Current second value
    private bool isTimerActive = false; // A flag to check if the timer is active

    public AudioSource timerMusic;  // Reference to the AudioSource for timer music
    public Action OnTimerEnd; // Event triggered when the timer reaches 0

    void Start()
    {
        // Initialize and start the timer
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

            // When time reaches 0, stop the timer and trigger the event
            if (timeRemaining <= 0)
            {
                StopTimer(); // Stop the timer when it reaches zero
                OnTimerEnd?.Invoke(); // Invoke the event when the timer ends
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

        // Start playing the timer music
        if (timerMusic != null)
        {
            timerMusic.Play();
        }
    }

    // Stop the timer and stop the music when the correct option is selected or timer ends
    public void StopTimer()
    {
        isTimerActive = false;

        // Stop the timer music when the timer is stopped
        if (timerMusic != null)
        {
            timerMusic.Stop();
        }
    }

    // Update the TextMeshPro text with the format "x detik"
    void UpdateTimerText()
    {
        timerText.text = currentSecond.ToString() + " detik";
    }
}
