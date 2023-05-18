using System;
using System.Collections.Generic;

class Question
{
    public string Text { get; }
    public string[] Answers { get; }
    private bool[] correctAnswers;

    public Question(string text, string[] answers, string[] correctAnswerIndices)
    {
        Text = text;
        Answers = answers;
        correctAnswers = new bool[answers.Length];

        foreach (string index in correctAnswerIndices)
        {
            int i = int.Parse(index.Trim()) - 1;
            correctAnswers[i] = true;
        }
    }

    public bool IsCorrect(int index)
    {
        return correctAnswers[index];
    }

    public override string ToString()
    {
        string answerString = string.Join(",", Answers);
        string correctAnswerString = string.Join(",", GetCorrectAnswerIndices());

        return $"{Text} | {answerString} | {correctAnswerString}";
    }

    private List<int> GetCorrectAnswerIndices()
    {
        List<int> indices = new List<int>();

        for (int i = 0; i < correctAnswers.Length; i++)
        {
            if (correctAnswers[i])
            {
                indices.Add(i + 1);
            }
        }

        return indices;
    }
}
