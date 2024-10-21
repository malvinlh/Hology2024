using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CardSpawnManager : MonoBehaviour
{
    public GameplayManager gameplayManager; // Reference to the GameplayManager script
    public CardZoom cardZoom; // Reference to the CardZoom script
    public UIElementShake uIElementShake;

    public GameObject card; // Reference to the single card GameObject
    public GameObject WinPanel; // Reference to the WinPanel GameObject
    public GameObject losePanel; // Reference to the losePanel GameObject
    public GameObject QuestionPanel; // Reference to the QuestionPanel GameObject
    public GameObject ThemePanel; // Reference to the ThemePanel GameObject
    public GameObject CorrectIcon; // Reference to the CorrectIcon GameObject
    public GameObject WrongIcon; // Reference to the WrongIcon GameObject
    public GameObject[] heartIcons;

    public QuestionSO[] questions; // Array of all QuestionSO scriptable objects
    public Timer timer; // Reference to the Timer script

    private int currentQuestionIndex = 0; // Track the current question index
    private QuestionSO currentQuestion; // The current question being displayed

    public TextMeshProUGUI themeText; // Reference to the theme TextMeshPro for displaying the theme
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] optionTexts; // Array to display the options in TextMeshPro

    public Button[] optionButtons; // Buttons for the options

    private bool isCorrectAnswer = false; // Track if the last answer was correct
    private bool isTimeUp = false; // Track if the timer ran out without an answer
    public bool Heart0 = false;
    public bool Heart1 = false;
    public bool Heart2 = false;
    public bool Heart3 = false;
    public bool Heart4 = false;

    public AudioSource correctSFX;
    public AudioSource wrongSFX;
    public AudioSource UIButtonSFX;
    public AudioSource answerButtonSFX;
    public AudioSource winSFX;
    public AudioSource gameOverSFX;

    void Start()
    {
        // Subscribe to the OnTimerEnd event in the Timer script
        timer.OnTimerEnd += OnTimeOut;

        // Hide everything except the Theme Panel at the start
        card.SetActive(false);
        WinPanel.SetActive(false);
        losePanel.SetActive(false);
        QuestionPanel.SetActive(false);
        CorrectIcon.SetActive(false); // Hide the CorrectIcon at the start
        WrongIcon.SetActive(false); // Hide the WrongIcon at the start
        heartIcons[0].SetActive(false);
        heartIcons[1].SetActive(false);
        heartIcons[2].SetActive(false);
        heartIcons[3].SetActive(false);
        heartIcons[4].SetActive(false);

        // Start the sequence with the Theme Panel
        StartCoroutine(ShowThemePanel());
    }

    // Coroutine to show the Theme Panel, then move to the Question Panel after 5 seconds
    IEnumerator ShowThemePanel()
    {
        ThemePanel.SetActive(true); // Show the Theme Panel
        timer.StopTimer(); // Stop the timer

        // Pause everything for 5 seconds
        yield return new WaitForSeconds(5f);

        // Hide the Theme Panel
        ThemePanel.SetActive(false);

        // Now show the first question
        StartCoroutine(ShowQuestionPanel());
    }

    // Coroutine to show the Question Panel and pause for the first question
    IEnumerator ShowQuestionPanel()
    {
        if (currentQuestionIndex < questions.Length)
        {
            timer.StopTimer(); // Stop the timer

            // Reset flags
            isCorrectAnswer = false;
            isTimeUp = false;

            // Set the current question from the QuestionSO array
            currentQuestion = questions[currentQuestionIndex];
            questionText.text = currentQuestion.question;

            // Enable the QuestionPanel and card
            QuestionPanel.SetActive(true);
            card.SetActive(true);

            // Update UI elements with the question details
            UpdateQuestionUI();

            // Pause for 5 seconds before allowing interaction
            yield return new WaitForSeconds(5f);

            // Hide the QuestionPanel after the delay
            QuestionPanel.SetActive(false);

            // Now allow zooming and answering by resuming zoom and starting the timer
            cardZoom.ResetZoom();
            timer.ResetTimer();
        }
        else
        {
            // All questions have been answered, show the win panel
            WinPanel.SetActive(true);
            winSFX.Play();
            card.SetActive(false);
            timer.StopTimer();
            cardZoom.PauseZoom();
        }
    }

    // Update the question options and theme text
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

    // Called when an option is selected
    void OnOptionSelected(int selectedIndex)
    {
        if (selectedIndex == currentQuestion.correctOptionIndex)
        {
            // Mark the answer as correct
            isCorrectAnswer = true;

            // Play the correct SFX
            correctSFX.Play();

            // Stop the timer and reset the zoom
            timer.StopTimer();

            // Start the sequence again for the next question
            StartCoroutine(PrepareNextQuestion());
        }
        else
        {
            if (Heart0 == false)
            {
                heartIcons[0].SetActive(true);
                Heart0 = true;

                wrongSFX.Play();
                uIElementShake.ShakeUIElement();
                isCorrectAnswer = false;
            }
            else if (Heart1 == false)
            {
                heartIcons[1].SetActive(true);
                Heart1 = true;

                wrongSFX.Play();
                uIElementShake.ShakeUIElement();
                isCorrectAnswer = false;
            }
            else if (Heart2 == false)
            {
                heartIcons[2].SetActive(true);
                Heart2 = true;

                wrongSFX.Play();
                uIElementShake.ShakeUIElement();
                isCorrectAnswer = false;
            }
            else if (Heart3 == false)
            {
                heartIcons[3].SetActive(true);
                Heart3 = true;

                wrongSFX.Play();
                uIElementShake.ShakeUIElement();
                isCorrectAnswer = false;
            }
            else if (Heart4 == false)
            {
                heartIcons[4].SetActive(true);
                Heart4 = true;
                timer.StopTimer();
                cardZoom.PauseZoom();

                // game over
                gameOverSFX.Play();
                losePanel.SetActive(true);
            }
        }
    }

    // Called when the timer runs out (invoked from the Timer.cs script)
    void OnTimeOut()
    {
        // Mark that the timer ran out
        isTimeUp = true;
        timer.StopTimer();
        cardZoom.PauseZoom();

        if (Heart0 == false)
        {
            heartIcons[0].SetActive(true);
            Heart0 = true;

            wrongSFX.Play();

            // Reset the zoom and proceed to the next question
            StartCoroutine(PrepareNextQuestion());
        }
        else if (Heart1 == false)
        {
            heartIcons[1].SetActive(true);
            Heart1 = true;

            wrongSFX.Play();

            // Reset the zoom and proceed to the next question
            StartCoroutine(PrepareNextQuestion());
        }
        else if (Heart2 == false)
        {
            heartIcons[2].SetActive(true);
            Heart2 = true;
            
            wrongSFX.Play();

            // Reset the zoom and proceed to the next question
            StartCoroutine(PrepareNextQuestion());
        }
        else if (Heart3 == false)
        {
            heartIcons[3].SetActive(true);
            Heart3 = true;

            wrongSFX.Play();

            // Reset the zoom and proceed to the next question
            StartCoroutine(PrepareNextQuestion());
        }
        else if (Heart4 == false)
        {
            heartIcons[4].SetActive(true);
            Heart4 = true;

            // game over
            gameOverSFX.Play();
            losePanel.SetActive(true);
        }
    }

    // Coroutine to prepare for the next question and blink the CorrectIcon or WrongIcon if needed
    IEnumerator PrepareNextQuestion()
    {
        // Pause the zoom animation
        cardZoom.PauseZoom();

        // Hide the current QuestionPanel while preparing for the next question
        QuestionPanel.SetActive(false);

        // If the last answer was correct, blink the CorrectIcon twice
        if (isCorrectAnswer)
        {
            yield return StartCoroutine(BlinkCorrectIcon());
        }
        // If the timer ran out, blink the WrongIcon twice
        else if (isTimeUp)
        {
            yield return StartCoroutine(BlinkWrongIcon());
        }

        // Wait for 2 seconds before showing the next question
        yield return new WaitForSeconds(2f);

        // Increment the question index
        currentQuestionIndex++;

        // Show the next question
        StartCoroutine(ShowQuestionPanel());
    }

    // Coroutine to blink the CorrectIcon twice
    IEnumerator BlinkCorrectIcon()
    {
        for (int i = 0; i < 2; i++)
        {
            CorrectIcon.SetActive(true); // Show the icon
            yield return new WaitForSeconds(0.5f); // Wait for half a second
            CorrectIcon.SetActive(false); // Hide the icon
            yield return new WaitForSeconds(0.5f); // Wait for another half a second
        }
    }

    // Coroutine to blink the WrongIcon twice
    IEnumerator BlinkWrongIcon()
    {
        for (int i = 0; i < 2; i++)
        {
            WrongIcon.SetActive(true); // Show the icon
            yield return new WaitForSeconds(0.5f); // Wait for half a second
            WrongIcon.SetActive(false); // Hide the icon
            yield return new WaitForSeconds(0.5f); // Wait for another half a second
        }
    }
}
