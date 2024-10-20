using System.Collections;
using UnityEngine;

public class UIElementShake : MonoBehaviour
{
    public RectTransform uiElement; // The UI element to shake
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 10f;

    public void ShakeUIElement()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        Vector3 originalPos = uiElement.anchoredPosition;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;

            uiElement.anchoredPosition = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        uiElement.anchoredPosition = originalPos;
    }
}
