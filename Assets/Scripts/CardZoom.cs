using UnityEngine;

public class CardZoom : MonoBehaviour
{
    public RectTransform cardTransform; // Reference to the card's RectTransform
    public Vector2 targetSize = new Vector2(1920, 1080); // Target size (1920x1080)
    public float zoomDuration = 60f; // Duration for the zoom to complete (in seconds)

    private Vector2 initialSize;
    private float timer = 0f;
    private bool isZooming = true;

    // Delegate to notify when the zoom finishes
    public delegate void ZoomFinished(CardZoom cardZoom);
    public ZoomFinished OnZoomFinished;

    void Start()
    {
        // Set the initial size of the card (192x108)
        initialSize = new Vector2(192, 108);
        cardTransform.sizeDelta = initialSize;
    }

    void Update()
    {
        // Zoom logic, only if the zooming process is still active
        if (isZooming)
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
}