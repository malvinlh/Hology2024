using UnityEngine;
using System.Collections;

public class CardFlip : MonoBehaviour
{
    public GameObject frontSide;  // Reference to the front side of the card
    public GameObject backSide;   // Reference to the back side of the card
    public float flipDuration = 0.5f;  // Duration of the flip animation

    private bool isFlipping = false;   // Flag to prevent multiple flips at the same time
    public bool isShowingFront = true; // Flag to track if the front is showing

    // Coroutine to flip the card
    public IEnumerator FlipCard()
    {
        if (isFlipping) yield break;

        isFlipping = true;

        float timeElapsed = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion middleRotation = Quaternion.Euler(0f, 90f, 0f);  // Rotate halfway (90 degrees)
        Quaternion endRotation = Quaternion.Euler(0f, 180f, 0f);    // Final rotation

        // Rotate to halfway
        while (timeElapsed < flipDuration / 2f)
        {
            transform.rotation = Quaternion.Lerp(startRotation, middleRotation, timeElapsed / (flipDuration / 2f));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = middleRotation;

        // Toggle the front and back sides
        frontSide.SetActive(isShowingFront);  // If the front is showing, show it
        backSide.SetActive(!isShowingFront);  // Show the back if the front is not showing

        // Rotate to final position
        timeElapsed = 0f;
        while (timeElapsed < flipDuration / 2f)
        {
            transform.rotation = Quaternion.Lerp(middleRotation, endRotation, timeElapsed / (flipDuration / 2f));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;

        isShowingFront = !isShowingFront;
        isFlipping = false;
    }

    // Flip the card instantly (used when loading state)
    public void FlipCardInstant()
    {
        if (!isShowingFront) return;  // If already flipped, do nothing

        // Flip the front and back instantly
        frontSide.SetActive(false);
        backSide.SetActive(true);

        isShowingFront = false;  // The card is now showing the back side
    }
}