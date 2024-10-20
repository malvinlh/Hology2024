using UnityEngine;

public class CardZoom : MonoBehaviour
{
    public RectTransform cardTransform; // Reference to the card's RectTransform
    public Vector2 targetSize = new Vector2(0, 0); // Target size (0x0)
    public float zoomDuration = 10f; // Duration for the zoom to complete (in seconds)

    public Vector2 initialSize;
    private float timer = 0f;
    private bool isZooming = true;
    private bool isPaused = false; // Flag to pause the zoom

    // Delegate to notify when the zoom finishes
    public delegate void ZoomFinished(CardZoom cardZoom);
    public ZoomFinished OnZoomFinished;

    void Start()
    {
        // Set the initial size of the card
        initialSize = new Vector2(1920, 1080);
        cardTransform.sizeDelta = initialSize;
    }

    void Update()
    {
        // Zoom logic, only if the zooming process is active and not paused
        if (isZooming && !isPaused)
        {
            timer += Time.deltaTime;
            float t = timer / zoomDuration;
            cardTransform.sizeDelta = Vector2.Lerp(initialSize, targetSize, t);

            // When the card reaches the target size, stop zooming and notify the manager
            if (cardTransform.sizeDelta == targetSize)
            {
                isZooming = false;
                OnZoomFinished?.Invoke(this); // Notify the manager
            }
        }
    }

    // Method to reset the zoom for a new question
    public void ResetZoom()
    {
        timer = 0f; // Reset the timer
        isZooming = true; // Enable zooming
        isPaused = false; // Ensure zooming is not paused
        initialSize = new Vector2(1920, 1080); // Reset initial size
        cardTransform.sizeDelta = initialSize; // Apply the reset size immediately
    }

    // Method to pause the zoom animation
    public void PauseZoom()
    {
        isPaused = true; // Pause zooming
    }

    // Method to resume the zoom animation
    public void ResumeZoom()
    {
        isPaused = false; // Resume zooming
    }
}
