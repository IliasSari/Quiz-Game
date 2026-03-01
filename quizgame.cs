using System;
using System.Collections.Generic;

class Question
{
    public string Text;
    public string[] Options;
    public int CorrectOption; // index (0-3)

    public Question(string text, string[] options, int correctOption)
    {
        Text = text;
        Options = options;
        CorrectOption = correctOption;
    }
}

class Program
{
    static void Main()
    {
        List<Question> questions = new List<Question>()
        {
            new Question(
                "Which is the Capital of Greece?",
                new string[] { "Thessaloniki", "Patra", "Athens", "Larissa" },
                2
            ),
            new Question(
                "How many continets are there on Earth?",
                new string[] { "5", "6", "7", "8" },
                2
            ),
            new Question(
                "Which is the biggest island of Greece?",
                new string[] { "Rhodes", "Evoia", "Crete", "Lesbos" },
                2
            ),
            new Question(
                "Which is the result of 5 * 6",
                new string[] { "11", "30", "56", "25" },
                1
            )
        };

        int score = 0;
        int lives = 3; // Added variables for lives

        Console.WriteLine("🎯 Welcome to the Quiz Game!");
        Console.WriteLine("-----------------------------");

        for (int i = 0; i < questions.Count; i++)
        {
            Console.WriteLine($"\n ❤️ Lives: {lives} | Score: {score}");
            Question q = questions[i];

            Console.WriteLine($"\nQuestion {i + 1}: {q.Text}");

            for (int j = 0; j < q.Options.Length; j++)
            {
                Console.WriteLine($"{j + 1}. {q.Options[j]}");
            }

            Console.Write("Your answer (1-4): ");
            string input = Console.ReadLine()?.Trim();

            int userChoice;
            bool valid = int.TryParse(input, out userChoice);

            if (valid && userChoice - 1 == q.CorrectOption)
            {
                Console.WriteLine("✅ Correct");
                score++;
            }
            else
            {
                lives--; 
                Console.WriteLine($"❌ Wrong! The correct answer is: {q.Options[q.CorrectOption]}");
                if (lives <=0)
                {
                    Console.WriteLine("\n 💔 GAME OVER! You ran out of lives.");
                    break;
                }
            }
        }

        Console.WriteLine("\n=======================");
        Console.WriteLine($"🏁 End of game!");
        Console.WriteLine($"🎉 Score: {score}/{questions.Count}");
        Console.WriteLine("=======================");

        Console.ReadLine();
    }
}