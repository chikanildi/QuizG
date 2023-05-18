using System;

class UserInterface
{
    public static void ShowWelcomeMessage()
    {
        Console.WriteLine("Welcome to the Quiz Program!");
    }

    public static void ShowQuizIntroduction()
    {
        Console.WriteLine("Let's play the quiz!");
    }

    public static void ShowNoMoreQuestionsMessage()
    {
        Console.WriteLine("There are no more questions.");
    }

    public static void ShowQuestion(Question question)
    {
        Console.WriteLine(question.Text);

        for (int i = 0; i < question.Answers.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {question.Answers[i]}");
        }
    }

    public static void ShowCorrectAnswerMessage()
    {
        Console.WriteLine("Correct!");
    }

    public static void ShowIncorrectAnswerMessage()
    {
        Console.WriteLine("Incorrect.");
    }

    public static void ShowCurrentScore(int score)
    {
        Console.WriteLine($"Your score is {score}.");
    }

    public static void ShowFinalScore(int score)
    {
        Console.WriteLine($"Your final score is {score}.");
    }

    public static void ShowThanksMessage()
    {
        Console.WriteLine("Thanks for playing the Quiz Program!");
    }

    public static void ShowNewLine()
    {
        Console.WriteLine();
    }

    public static string GetQuestionFromUser()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Enter a question (or 'quit' to finish):");
                string input = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Invalid input. Please enter a valid question.");
                    continue;
                }

                return input;
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred while reading the input. Please try again.");
                Console.WriteLine(e.Message);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid input format. Please try again.");
                Console.WriteLine(e.Message);
            }
        }
    }


    public static bool Quit(string input)
    {
        return input.ToLower() == "quit";
    }

    public static string[] GetAnswersFromUser()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Enter the multiple choice answers separated by commas:");
                return Console.ReadLine().Split(',');
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred while reading the input. Please try again.");
                Console.WriteLine(e.Message);
            }
        }
    }

    public static string[] GetCorrectAnswersFromUser()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Enter the index(es) of the correct answer(s) separated by commas:");
                return Console.ReadLine().Split(',');
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred while reading the input. Please try again.");
                Console.WriteLine(e.Message);
            }
        }
    }

    public static string GetAnswerFromUser()
    {
        while (true)
        {
            try
            {
                Console.Write("Enter your answer(s) separated by commas: ");
                return Console.ReadLine().Trim();
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred while reading the input. Please try again.");
                Console.WriteLine(e.Message);
            }
        }
    }

    public static string GetContinueInput()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Press enter to continue to the next question or type 'quit' to end the quiz.");
                return Console.ReadLine().Trim();
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred while reading the input. Please try again.");
                Console.WriteLine(e.Message);
            }
        }
    }
}
