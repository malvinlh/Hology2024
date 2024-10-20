using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorSprite; // Assign your custom cursor sprite in the Inspector
    public Vector2 hotspot = Vector2.zero; // Define the "click point" of the cursor (hotspot)
    public CursorMode cursorMode = CursorMode.ForceSoftware; // Use the default cursor mode

    void Start()
    {
        SetCustomCursor();
    }

    public void SetCustomCursor()
    {
        // Set the custom cursor with the sprite and hotspot
        Cursor.SetCursor(cursorSprite, hotspot, cursorMode);
    }
}