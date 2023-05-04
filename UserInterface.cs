using System;
using System.Collections.Generic;

namespace Quiz_05_03
{
    class UserInterface
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Quiz Program!");

            QuizGame quizGame = new QuizGame();

            bool isAddingQuestions = true;
            while (isAddingQuestions)
            {
                string questionText = GetQuestionText();

                if (Quit(questionText))
                {
                    break;
                }

                string[] answers = GetAnswers();

                string[] correctAnswers = GetCorrectAnswers();

                Question question = new Question(questionText, answers, correctAnswers);
                quizGame.AddQuestion(question);
            }

            int score = quizGame.PlayQuiz();

            DisplayScore(score);

            Console.WriteLine("Thanks for playing the Quiz Program!");
        }

        static string GetQuestionText()
        {
            Console.WriteLine("Enter a question (or 'quit' to finish):");
            return Console.ReadLine().Trim();
        }

        static bool Quit(string input)
        {
            return input.ToLower() == "quit";
        }

        static string[] GetAnswers()
        {
            Console.WriteLine("Enter the multiple choice answers separated by commas:");
            return Console.ReadLine().Split(',');
        }

        static string[] GetCorrectAnswers()
        {
            Console.WriteLine("Enter the index(es) of the correct answer(s) separated by commas:");
            return Console.ReadLine().Split(',');
        }

        static void DisplayScore(int score)
        {
            Console.WriteLine($"Your score is {score}.");
        }
    }
}
