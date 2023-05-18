using System;
using System.Collections.Generic;

class QuizProgram
{
    private const int SCORE = 25;

    static void Main(string[] args)
    {
        UserInterface.ShowWelcomeMessage();

        List<Question> questions = new List<Question>();

        while (true)
        {
            string questionText = UserInterface.GetQuestionFromUser();

            if (UserInterface.Quit(questionText))
            {
                break;
            }

            string[] answers = UserInterface.GetAnswersFromUser();
            string[] correctAnswers = UserInterface.GetCorrectAnswersFromUser();

            Question question = new Question(questionText, answers, correctAnswers);
            questions.Add(question);
        }

        using (FileHandler fileHandler = new FileHandler("questions.txt"))
        {
            fileHandler.WriteQuestionsToFile(questions);

            List<Question> questionsFromFile = fileHandler.ReadQuestionsFromFile();

            int score = 0;
            UserInterface.ShowQuizIntroduction();

            while (questionsFromFile.Count > 0)
            {
                if (questionsFromFile.Count == 0)
                {
                    UserInterface.ShowNoMoreQuestionsMessage();
                    break;
                }

                int questionIndex = new Random().Next(questionsFromFile.Count);
                Question question = questionsFromFile[questionIndex];
                questionsFromFile.RemoveAt(questionIndex);

                UserInterface.ShowQuestion(question);

                string answerInput = UserInterface.GetAnswerFromUser();

                if (UserInterface.Quit(answerInput))
                {
                    break;
                }

                string[] userAnswers = answerInput.Split(',');

                bool isCorrect = true;

                foreach (string userAnswer in userAnswers)
                {
                    int index = int.Parse(userAnswer.Trim()) - 1;

                    if (!question.IsCorrect(index))
                    {
                        isCorrect = false;
                        break;
                    }
                }

                if (isCorrect)
                {
                    UserInterface.ShowCorrectAnswerMessage();
                    score += SCORE;
                }
                else
                {
                    UserInterface.ShowIncorrectAnswerMessage();
                }

                UserInterface.ShowCurrentScore(score);
                UserInterface.ShowNewLine();

                string continueInput = UserInterface.GetContinueInput();

                if (UserInterface.Quit(continueInput))
                {
                    break;
                }

                UserInterface.ShowNewLine();
            }

            UserInterface.ShowFinalScore(score);
            UserInterface.ShowThanksMessage();
        }
    }
}
