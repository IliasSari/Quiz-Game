using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; 
using Newtonsoft.Json;

namespace QuizGame
{
    public class Question
    {
        public string Text { get; set; } = string.Empty;
        public string[] Options { get; set; } = Array.Empty<string>();
        public int CorrectOption { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "questions.json";

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"❌ Archieve {filePath} not found!");
                return;
            }

            string json = File.ReadAllText(filePath);
            List<Question>? questions = JsonConvert.DeserializeObject<List<Question>>(json);

            if (questions == null || questions.Count == 0)
            {
                Console.WriteLine("❌ Questions didn't found.");
                return;
            }

            int score = 0;
            int lives = 2;

            Console.WriteLine("🎯 Welcome to Quiz Game!");
            
            Random rng = new Random();
            questions = questions.OrderBy(x => rng.Next()).ToList();
            Console.WriteLine("🎲 Questions shuffled!");

            for (int i = 0; i < questions.Count; i++)
            {
                Console.WriteLine($"\n ❤️ Lives: {lives} | Score: {score}");
                Question q = questions[i];

                Console.WriteLine($"\nQuestion {i + 1}: {q.Text}");

                for (int j = 0; j < q.Options.Length; j++)
                {
                    Console.WriteLine($"{j + 1}. {q.Options[j]}");
                }

                Console.Write("Answer (1-4): ");
                string? input = Console.ReadLine();

                int userChoice;
                bool valid = int.TryParse(input, out userChoice);

                if (valid && userChoice - 1 == q.CorrectOption)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("✅ Correct!");
                    Console.ResetColor();
                    score++;
                }
                else
                {
                    lives--;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"❌ Wrong! Correct answer was: {q.Options[q.CorrectOption]}");
                    Console.ResetColor();

                    if (lives <= 0)
                    {
                        Console.WriteLine("\n💔 Game Over! Lives are over.");
                        break;
                    }
                }
            }

            Console.WriteLine("\n=======================");
            Console.WriteLine($"🏁 Game over! Score: {score}/{questions.Count}");
            Console.WriteLine("=======================");
        }
    }
}