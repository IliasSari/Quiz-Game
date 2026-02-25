using System;

class quizgame
{
    static void Main()
    {
        Console.WriteLine("--- Welcome to the Quiz Game! ---");

        string[] questions = {
            "What is the Capital of Greece?",
            "Which planet is known as the Red Planet?",
            "How many continents are there on Earth?"
        };

        string[] answers = {
            "athens",
            "mars",
            "7"
        };

        int score = 0; 

        for (int i = 0; i < questions.Length; i++)
        {
            Console.WriteLine("\nQuestion " + (i + 1));
            Console.WriteLine(questions[i]);

            Console.Write("Your Answer: ");
            string userAnswer = Console.ReadLine();

            if (userAnswer.ToLower().Trim() == answers[i])
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Correct!");
                Console.ResetColor();
                score++;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong! The correct answer was: " + answers[i]);
                Console.ResetColor();
            }
        
        }
        if (score == questions.Length)
        {
            Console.WriteLine("Perfect! You are a genius!");
        }
        else if (score >= questions.Length/2)
        {
            Console.WriteLine("Not bad. You know your stuff.");
        }
        else
        {
            Console.WriteLine("Better luck next time. Keep studying.");
        }

        Console.WriteLine("\n--- Game Over! ---");
        Console.WriteLine("Your total score is: " + score + "/" + questions.Length);
    }
}