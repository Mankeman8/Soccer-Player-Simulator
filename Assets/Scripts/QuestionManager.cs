using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText;
    public List<Answer> answers;
}

[System.Serializable]
public class Answer
{
    public string text;
    public int value;
}

[System.Serializable]
public class QuestionList
{
    public List<Question> questions;
}

public class QuestionManager : MonoBehaviour
{
    private QuestionList questionList;
    private Question currentQuestion;

    void Start()
    {
        string json = Resources.Load<TextAsset>("Questions").ToString();
        questionList = JsonUtility.FromJson<QuestionList>(json);
    }

    public Question GetCurrentQuestion()
    {
        return currentQuestion;
    }

    public Question GetRandomQuestion()
    {
        int index = Random.Range(0, questionList.questions.Count);
        currentQuestion = questionList.questions[index];
        questionList.questions.RemoveAt(index); // Removes the question from the list to avoid duplication
        return currentQuestion;
    }
}
