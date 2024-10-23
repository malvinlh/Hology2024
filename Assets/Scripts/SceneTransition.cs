using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Image fadeOutImage; // Assign the image that fades out the current scene
    public Image fadeInImage;  // Assign the image that fades in the next scene
    public float fadeDuration = 1f; // How long the fade should take
    public string nextSceneName = "NextScene"; // The name of the next scene to load

    // Crossfade coroutine to blend from one scene to another
    private IEnumerator Crossfade()
    {
        // Ensure both images are active
        fadeOutImage.gameObject.SetActive(true);
        fadeInImage.gameObject.SetActive(true);

        Color fadeOutColor = fadeOutImage.color;
        Color fadeInColor = fadeInImage.color;

        // Set initial alpha values
        fadeOutColor.a = 0f; // Fully transparent (start visible)
        fadeInColor.a = 1f;  // Fully opaque (start hidden)
        fadeOutImage.color = fadeOutColor;
        fadeInImage.color = fadeInColor;

        // Crossfade both images over time
        for (float t = 0.0f; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            fadeOutColor.a = Mathf.Lerp(0f, 1f, normalizedTime); // Fade-out from transparent to opaque
            fadeInColor.a = Mathf.Lerp(1f, 0f, normalizedTime);  // Fade-in from opaque to transparent

            fadeOutImage.color = fadeOutColor;
            fadeInImage.color = fadeInColor;

            yield return null;
        }

        // Ensure full opacity/visibility at the end
        fadeOutColor.a = 1f;
        fadeInColor.a = 0f;
        fadeOutImage.color = fadeOutColor;
        fadeInImage.color = fadeInColor;

        // Now load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
