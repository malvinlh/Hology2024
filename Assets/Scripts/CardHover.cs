using UnityEngine;
using UnityEngine.EventSystems;  // Needed for PointerEnter/PointerExit/PointerClick
using UnityEngine.SceneManagement;  // Needed for scene loading
using UnityEngine.UI;

public class CardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Outline frontCardOutline;  // Reference to the Outline component
    public Outline backCardOutline;  // Reference to the Outline component
    public GameObject confirmationPanel; // Reference to the Confirmation Panel GameObject
    public string sceneToLoad;  // Scene to load when "Yes" is clicked

    private bool isCardClicked = false;  // Flag to track if the card is clicked

    public AudioSource UIButtonSFX;
    public AudioSource answerButtonSFX;

    void Start()
    {
        // Ensure the outline is initially disabled (no border)
        if (frontCardOutline != null || backCardOutline != null)
        {
            frontCardOutline.enabled = false;
            backCardOutline.enabled = false;
        }

        // Ensure the Confirmation Panel is disabled at the start
        if (confirmationPanel != null)
        {
            confirmationPanel.SetActive(false);
        }
    }

    // Called when the mouse enters the card's area (for UI elements)
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Enable the outline when hovering over the card
        if (frontCardOutline != null || backCardOutline != null)
        {
            frontCardOutline.enabled = true;
            backCardOutline.enabled = true;
        }
    }

    // Called when the mouse exits the card's area (for UI elements)
    public void OnPointerExit(PointerEventData eventData)
    {
        // Disable the outline when the mouse is no longer hovering
        if (frontCardOutline != null || backCardOutline != null)
        {
            frontCardOutline.enabled = false;
            backCardOutline.enabled = false;
        }
    }

    // Called when the card is clicked (for UI elements)
    public void OnPointerClick(PointerEventData eventData)
    {
        // Enable the Confirmation Panel when the card is clicked
        if (confirmationPanel != null)
        {
            UIButtonSFX.Play();
            confirmationPanel.SetActive(true);
            isCardClicked = true;
        }
    }

    // This method will be called by the "Yes" button in the confirmation panel
    public void OnYesButtonClick()
    {
        if (isCardClicked && !string.IsNullOrEmpty(sceneToLoad))
        {
            answerButtonSFX.Play();
            
            // Load the appropriate scene based on the card that was clicked
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
        }
    }

    // This method will be called by the "No" button in the confirmation panel
    public void OnNoButtonClick()
    {
        // Simply hide the confirmation panel if the player clicks "No"
        if (confirmationPanel != null)
        {
            answerButtonSFX.Play();
            confirmationPanel.SetActive(false);
            isCardClicked = false;
        }
    }
}