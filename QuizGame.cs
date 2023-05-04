using System;
using System.Collections.Generic;
using System.IO;

class QuizGame
{
    private const int SCORE = 20;
    private List<Question> questions;

    public QuizGame()
    {
        questions = new List<Question>();
    }

    public void AddQuestion(Question question)
    {
        questions.Add(question);
    }

    public void WriteQuestionsToFile(string fileName)
    {
        FileHandler.WriteQuestionsToFile(fileName, questions);
    }

    public List<Question> ReadQuestionsFromFile(string fileName)
    {
        return FileHandler.ReadQuestionsFromFile(fileName);
    }

    public int PlayQuiz()
    {
        int score = 0;
        Console.WriteLine("Let's play the quiz!");

        List<Question> questionsFromFile = new List<Question>(questions);

        while (questionsFromFile.Count > 0)
        {
            if (questionsFromFile.Count == 0)
            {
                Console.WriteLine("There are no more questions.");
                break;
            }

            Question question = GetRandomQuestion(questionsFromFile);

            questionsFromFile.Remove(question);

            DisplayQuestion(question);

            string answerInput = GetAnswerInput();

            if (Quit(answerInput))
            {
                break;
            }

            string[] userAnswers = answerInput.Split(',');

            bool isCorrect = CheckAnswers(question, userAnswers);

            if (isCorrect)
            {
                DisplayCorrectAnswer();
                score += SCORE;
            }
            else
            {
                DisplayIncorrectAnswer();
            }

            DisplayScore(score);

            if (QuitOrContinue())
            {
                break;
            }
        }

        DisplayScore(score);

        return score;
    }

    private Question GetRandomQuestion(List<Question> questions)
    {
        int questionIndex = new Random().Next(questions.Count);
        return questions[questionIndex];
    }

    private void DisplayQuestion(Question question)
    {
        Console.WriteLine(question.Text);

        for (int i = 0; i < question.Answers.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {question.Answers[i]}");
        }
    }

    private string GetAnswerInput()
    {
        Console.Write("Enter your answer(s) separated by commas: ");
        return Console.ReadLine().Trim();
    }

    private bool CheckAnswers(Question question, string[] userAnswers)
    {
        foreach (string userAnswer in userAnswers)
        {
            int index = int.Parse(userAnswer.Trim()) - 1;

            if (!question.IsCorrect(index))
            {
                return false;
            }
        }

        return true;
    }

    private void DisplayCorrectAnswer()
    {
        Console.WriteLine("Correct!");
    }

    private void DisplayIncorrectAnswer()
    {
        Console.WriteLine("Incorrect.");
    }

    private void DisplayScore(int score)
    {
        Console.WriteLine($"Your score is {score}.");
    }

    private bool QuitOrContinue()
    {
        Console.WriteLine("Press enter to continue to the next question or type 'quit' to end the quiz.");
        string continueInput = Console.ReadLine().Trim();

        return Quit(continueInput);
    }

    private bool Quit(string input)
    {
        return input.ToLower() == "quit";
    }
}

class FileHandler
{
    public static void WriteQuestionsToFile(string fileName, List<Question> questions)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (Question question in questions)
            {
                writer.WriteLine(question.ToString());
            }
        }
    }

    public static List<Question> ReadQuestionsFromFile(string fileName)
    {
        List<Question> questionsFromFile = new List<Question>();

        using (StreamReader reader = new StreamReader(fileName))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] parts = line.Split('|');
                string questionText = parts[0].Trim();
                string[] answers = parts[1].Split(',');
                string[] correctAnswers = parts[2].Split(',');

                Question question = new Question(questionText, answers, correctAnswers);
                questionsFromFile.Add(question);
            }
        }

        return questionsFromFile;
    }
}

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