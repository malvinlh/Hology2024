using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public int cardIndex; // The index of the card that corresponds to this gameplay scene

    // Call this when the player finishes the level
    public void OnLevelComplete()
    {
        // Save the flip state of the card in PlayerPrefs
        PlayerPrefs.SetInt("CardFlipped" + cardIndex, 1);
        PlayerPrefs.Save();
    }
}