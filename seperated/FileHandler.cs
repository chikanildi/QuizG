using System;
using System.Collections.Generic;
using System.IO;

class FileHandler : IDisposable
{
    private string filePath;

    public FileHandler(string filePath)
    {
        this.filePath = filePath;
    }

    public void WriteQuestionsToFile(List<Question> questions)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Question question in questions)
                {
                    writer.WriteLine(question.ToString());
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"File not found: {ex.Message}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Unauthorized access to the file: {ex.Message}");
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine($"Directory not found: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    public List<Question> ReadQuestionsFromFile()
    {
        List<Question> questions = new List<Question>();

        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split('|');

                    string questionText = parts[0].Trim();
                    string[] answers = parts[1].Split(',');
                    string[] correctAnswers = parts[2].Split(',');

                    Question question = new Question(questionText, answers, correctAnswers);
                    questions.Add(question);
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"File not found: {ex.Message}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Unauthorized access to the file: {ex.Message}");
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine($"Directory not found: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"An error occurred while reading from the file: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }

        return questions;
    }

    public void Dispose()
    {
        // Clean up resources if needed
    }
}
