using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public string SceneName; // Name of the scene to load
    public void LoadScene(string sceneName)
    {
        // Load the scene with the given name
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
