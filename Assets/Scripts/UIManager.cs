using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public QuestionManager questionManager;
    public TMP_Text questionTextUI;
    public TMP_Dropdown answerDropdownUI;
    public Button nextButton;
    public Text counterText;
    public int answerCount;

    private void Awake()
    {
        answerCount = 0;
    }

    void Start()
    {
        nextButton.onClick.AddListener(NextQuestion);
        DisplayRandomQuestion();
    }

    public void DisplayRandomQuestion()
    {
        Question question = questionManager.GetRandomQuestion();
        questionTextUI.text = question.questionText;
        answerDropdownUI.ClearOptions();

        List<string> answerTexts = new List<string>();
        foreach (Answer answer in question.answers)
        {
            answerTexts.Add(answer.text);
        }
        answerDropdownUI.AddOptions(answerTexts);
    }

    public void NextQuestion()
    {
        //Find out which answer was chosen
        //grab the value from the JSON file
        //add it to answerCount
        //check if there's still questions
        //if so, go to next question
        //else, start game
        int selectedAnswerIndex = answerDropdownUI.value;
    }
}