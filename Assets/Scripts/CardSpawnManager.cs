using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardSpawnManager : MonoBehaviour
{
    public GameObject cardPrefab; // Prefab for the card object
    public RectTransform canvasTransform; // Reference to the canvas
    public QuestionSO[] questions; // Array of all QuestionSO scriptable objects
    public Timer timer; // Reference to the Timer script

    private GameObject currentCard; // Holds the current spawned card
    private QuestionSO currentQuestion; // The current question being displayed

    public TextMeshProUGUI themeText; // Reference to the theme TextMeshPro for displaying the theme
    public TextMeshProUGUI[] optionTexts; // Array to display the options in TextMeshPro
    
    public Button[] optionButtons; // Buttons for the options

    void Start()
    {
        // Spawn the first card when the game starts
        SpawnCard();
    }

    void SpawnCard()
    {
        // Randomly select a question from the array
        currentQuestion = questions[Random.Range(0, questions.Length)];

        // Instantiate the card at the (0,0) position of the canvas
        currentCard = Instantiate(cardPrefab, canvasTransform);

        // Set the card's position to (0, 0)
        RectTransform cardRectTransform = currentCard.GetComponent<RectTransform>();
        cardRectTransform.anchoredPosition = Vector2.zero;

        // Get the CardZoom component to handle zoom
        CardZoom cardZoom = currentCard.GetComponent<CardZoom>();

        // Update the card's image based on the current question
        cardZoom.cardTransform.GetComponent<UnityEngine.UI.Image>().sprite = currentQuestion.image;

        // Set the delegate to destroy the card when it finishes zooming
        cardZoom.OnZoomFinished += DestroyCard;

        // Update UI elements with the question details
        UpdateQuestionUI();

        // Reset the timer when a new card is spawned
        timer.ResetTimer();
    }

    void UpdateQuestionUI()
    {
        // Update theme text
        themeText.text = currentQuestion.theme;

        // Update the options text and assign button click events
        for (int i = 0; i < optionTexts.Length; i++)
        {
            if (i < currentQuestion.options.Length)
            {
                optionTexts[i].text = currentQuestion.options[i];
                optionButtons[i].gameObject.SetActive(true);

                // Remove any existing listeners to avoid multiple calls
                optionButtons[i].onClick.RemoveAllListeners();

                // Assign the button click to check if it's the correct option
                int optionIndex = i; // Capture the index for the button
                optionButtons[i].onClick.AddListener(() => OnOptionSelected(optionIndex));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false); // Hide unused buttons
            }
        }
    }

    // This function is called when an option button is pressed
    void OnOptionSelected(int selectedIndex)
    {
        // Check if the selected index is the correct answer
        if (selectedIndex == currentQuestion.correctOptionIndex)
        {
            Debug.Log("Correct Answer!");

            // Stop the timer if the correct option is selected
            timer.StopTimer();

            DestroyCard(currentCard.GetComponent<CardZoom>());
        }
        else
        {
            Debug.Log("Wrong Answer. Try again.");
        }
    }

    void DestroyCard(CardZoom cardZoom)
    {
        // Destroy the card when the zoom is finished or correct option is selected
        Destroy(cardZoom.gameObject);

        // Spawn a new card with a new question after the previous one finishes
        SpawnCard();
    }
}
