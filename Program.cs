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
                Console.WriteLine($"❌ File {filePath} not found!");
                return;
            }

            string json = File.ReadAllText(filePath);
            List<Question>? questions = JsonConvert.DeserializeObject<List<Question>>(json);

            if (questions == null || questions.Count == 0)
            {
                Console.WriteLine("❌ No questions found.");
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
                if (i > 0) NextQuestionEffect();

                Console.WriteLine($"\n ❤️ Lives: {lives} | Score: {score}");
                Question q = questions[i];

                string correctAnswText = q.Options[q.CorrectOption]; 
                q.Options = q.Options.OrderBy(x => rng.Next()).ToArray(); 
                q.CorrectOption = Array.IndexOf(q.Options, correctAnswText); 

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
                    Console.WriteLine($"❌ Wrong! Correct answer was: {correctAnswText}");
                    Console.ResetColor();

                    if (lives <= 0)
                    {
                        Console.WriteLine("\n💔 Game Over! You ran out of lives.");
                        break;
                    }
                }
            }

            Console.WriteLine("\n=======================");
            Console.WriteLine($"🏁 Game over! Score: {score}/{questions.Count}");
            Console.WriteLine("=======================");
            
            HandleHighScore(score);
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void NextQuestionEffect()
        {
            Console.WriteLine("\nPress Enter for the next question...");
            Console.ReadLine();
            Console.Clear();
        }

        static void HandleHighScore(int score)
        {
            string highscoreFile = "highscore.txt";
            int topScore = 0;

            if (File.Exists(highscoreFile))
            {
                string content = File.ReadAllText(highscoreFile);
                int.TryParse(content, out topScore);
            }

            Console.WriteLine($"🏆 High Score: {topScore}");

            if (score > topScore)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("🌟 New high score! Congratulations!");
                Console.ResetColor();
                File.WriteAllText(highscoreFile, score.ToString());
            }
        }
    }
}