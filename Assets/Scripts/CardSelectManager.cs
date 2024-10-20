using UnityEngine;

public class CardSelectManager : MonoBehaviour
{
    public CardFlip[] cards; // Array of cards in the CardSelect scene

    void Start()
    {
        // Load the saved flip states when the scene starts
        LoadCardStates();
    }

    void LoadCardStates()
    {
        // Loop through all the cards and check if they should be flipped
        for (int i = 0; i < cards.Length; i++)
        {
            // Check if this card should be flipped (based on PlayerPrefs)
            if (PlayerPrefs.GetInt("CardFlipped" + i, 0) == 1)
            {
                cards[i].FlipCardInstant(); // Flip the card instantly
            }
        }
    }

    // Call this when a specific gameplay scene is completed
    public void MarkCardAsFlipped(int cardIndex)
    {
        // Mark the card as flipped in PlayerPrefs
        PlayerPrefs.SetInt("CardFlipped" + cardIndex, 1);

        // Optionally, save the PlayerPrefs to disk immediately
        PlayerPrefs.Save();

        // Flip the card in the scene
        cards[cardIndex].FlipCard();
    }
}