using UnityEngine;
using System.Collections;

public class CardFlip : MonoBehaviour
{
    public GameObject frontSide;  // Reference to the front side of the card
    public GameObject backSide;   // Reference to the back side of the card
    public float flipDuration = 0.5f;  // Duration of the flip animation

    private bool isFlipping = false;   // Flag to prevent multiple flips at the same time
    public bool isShowingFront = true; // Public flag to track which side is currently visible

    // This method is triggered when the card is clicked
    public void OnCardClicked(CardFlipManager manager)
    {
        if (!isFlipping)
        {
            manager.FlipCard(this);
        }
    }

    // Coroutine to handle the card flip
    public IEnumerator FlipCard()
    {
        isFlipping = true;

        float timeElapsed = 0f;
        float halfDuration = flipDuration / 2f;

        // Initial rotation (start from current rotation)
        Quaternion startRotation = transform.rotation;
        Quaternion middleRotation = Quaternion.Euler(0f, 90f, 0f);  // Rotation halfway (90 degrees)
        Quaternion endRotation = Quaternion.Euler(0f, 180f, 0f);    // Final rotation (180 degrees)

        // Rotate halfway to 90 degrees Y
        while (timeElapsed < halfDuration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, middleRotation, timeElapsed / halfDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = middleRotation;

        // Switch the sides when the card is facing away (90 degrees Y)
        if (isShowingFront)
        {
            frontSide.SetActive(false);
            backSide.SetActive(true);
        }
        else
        {
            frontSide.SetActive(true);
            backSide.SetActive(false);
        }

        // Reset the time and continue the flip to 180 degrees Y
        timeElapsed = 0f;
        while (timeElapsed < halfDuration)
        {
            transform.rotation = Quaternion.Lerp(middleRotation, endRotation, timeElapsed / halfDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = endRotation;

        // Reset the rotation to 0 if we're showing the front side again
        if (!isShowingFront)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        isShowingFront = !isShowingFront;
        isFlipping = false;
    }

    // Method to flip the card back if it's showing the back side
    public IEnumerator FlipBack()
    {
        if (!isShowingFront && !isFlipping)
        {
            yield return StartCoroutine(FlipCard());
        }
    }
}