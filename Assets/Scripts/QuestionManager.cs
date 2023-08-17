using System.Collections.Generic;
using System.IO;
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

    void Start()
    {
        string path = Application.dataPath + "/Questions.json";
        string jsonString = File.ReadAllText(path);
        questionList = JsonUtility.FromJson<QuestionList>(jsonString);
    }

    public Question GetRandomQuestion()
    {
        int index = Random.Range(0, questionList.questions.Count);
        Question question = questionList.questions[index];
        questionList.questions.RemoveAt(index); // Removes the question from the list to avoid duplication
        return question;
    }
}
