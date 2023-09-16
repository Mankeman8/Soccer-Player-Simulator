using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using System.IO; // Required for File IO

[System.Serializable]
public class Personality
{
    public string title;
    public int minScore;
    public int maxScore;
    public string description;
}

[System.Serializable]
public class PersonalityList
{
    public List<Personality> personalities;
}

public class UIManager : MonoBehaviour
{
    [Header("Question Section")]
    public QuestionManager questionManager;
    public TMP_Text questionTextUI;
    public TMP_Dropdown answerDropdownUI;
    public Button nextButton;
    public TMP_Text counterText;

    [Header("Result Section")]
    public TMP_Text titleTextUI; // Text UI to display personality title
    public TMP_Text descriptionTextUI; // Text UI to display personality description
    public GameObject quizScreen;
    public GameObject resultScreen;

    private int questionCount;
    private int answerCount;

    public PersonalityList personalityList; // To store personalities from JSON

    private void Awake()
    {
        answerCount = 0;
        questionCount = 1;

        // Load personalities from JSON file (Assuming you have a file called "personality.json" in Resources folder)
        string json = Resources.Load<TextAsset>("Personality").ToString();
        personalityList = JsonUtility.FromJson<PersonalityList>(json);
    }

    void Start()
    {
        nextButton.onClick.AddListener(NextQuestion);
        DisplayRandomQuestion();
    }

    public void DisplayRandomQuestion()
    {
        if(questionCount<=10)
        {
            Question question = questionManager.GetRandomQuestion();
            questionTextUI.text = question.questionText;
            answerDropdownUI.ClearOptions();
            counterText.text = "Question " + questionCount + "/10";

            List<string> answerTexts = new List<string>();
            foreach (Answer answer in question.answers)
            {
                answerTexts.Add(answer.text);
            }
            answerDropdownUI.AddOptions(answerTexts);
        }
        else
        {
            DisplayResultScreen();
        }
    }

    public void NextQuestion()
    {
        int selectedAnswerIndex = answerDropdownUI.value;
        Question currentQuestion = questionManager.GetCurrentQuestion();
        Answer selectedAnswer = currentQuestion.answers[selectedAnswerIndex];
        answerCount += selectedAnswer.value;
        questionCount++;
        DisplayRandomQuestion();
    }

    public void DisplayResultScreen()
    {
        quizScreen.SetActive(false);
        resultScreen.SetActive(true);

        // Fetch the Personality based on the final score
        Personality finalPersonality = GetPersonalityByScore(answerCount);

        // Update UI elements with the personality data
        if (finalPersonality != null)
        {
            titleTextUI.text = finalPersonality.title;
            descriptionTextUI.text = finalPersonality.description;
        }
        else
        {
            titleTextUI.text = "Unknown";
            descriptionTextUI.text = "How did you manage to break my game?";
        }
    }

    public Personality GetPersonalityByScore(int score)
    {
        foreach (Personality personality in personalityList.personalities)
        {
            if (score >= personality.minScore && score <= personality.maxScore)
            {
                return personality;
            }
        }
        return null; // Return null or a default Personality if no match is found
    }
}