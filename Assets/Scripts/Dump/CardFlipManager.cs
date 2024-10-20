using UnityEngine;
using System.Collections;

public class CardFlipManager : MonoBehaviour
{
    private CardFlip currentlyFlippedCard = null;  // Stores the currently flipped card
    private bool isCardFlipping = false;           // Prevent flipping while another flip is in progress

    // Method to flip the selected card and handle flipping back the previous card
    public void FlipCard(CardFlip clickedCard)
    {
        if (isCardFlipping) return;  // Do not allow flipping when another flip is ongoing

        StartCoroutine(HandleCardFlipSequence(clickedCard));
    }

    private IEnumerator HandleCardFlipSequence(CardFlip clickedCard)
    {
        isCardFlipping = true;

        // Now flip the clicked card
        yield return StartCoroutine(clickedCard.FlipCard());

        // Update the currently flipped card reference
        currentlyFlippedCard = clickedCard.isShowingFront ? null : clickedCard;

        isCardFlipping = false;
    }
}
