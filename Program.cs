using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Diagnostics;

namespace QuizGame
{
    // Represents a single quiz question
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

            // Check if the JSON file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"❌ File {filePath} not found!");
                return;
            }

            // Read JSON file and parse it into a list of Question objects
            string json = File.ReadAllText(filePath);
            List<Question>? questions = JsonConvert.DeserializeObject<List<Question>>(json);

            // Check if any questions were loaded
            if (questions == null || questions.Count == 0)
            {
                Console.WriteLine("❌ No questions found.");
                return;
            }

            // Initialize game stats
            int score = 0;
            int lives = 3;

            Console.WriteLine("🎯 Welcome to Quiz Game!");

            // Shuffle the order of the questions randomly
            Random rng = new Random();
            questions = questions.OrderBy(x => rng.Next()).ToList();
            Console.WriteLine("🎲 Questions shuffled!");

            // Loop through each question
            for (int i = 0; i < questions.Count; i++)
            {
                // Pause and clear screen before showing the next question
                if (i > 0) NextQuestionEffect();

                Console.WriteLine($"\n ❤️ Lives: {lives} | Score: {score}");
                Question q = questions[i];

                // Save the text of the correct answer before shuffling options
                string correctAnswText = q.Options[q.CorrectOption];

                // Shuffle the options randomly
                q.Options = q.Options.OrderBy(x => rng.Next()).ToArray();

                // Find the new index of the correct answer after shuffling
                q.CorrectOption = Array.IndexOf(q.Options, correctAnswText);

                // Display the question
                Console.WriteLine($"\nQuestion {i + 1}: {q.Text}");

                // Display all options
                for (int j = 0; j < q.Options.Length; j++)
                {
                    Console.WriteLine($"{j + 1}. {q.Options[j]}");
                }

                // Get user input
                Console.Write("Answer (1-4): ");
                string? input = Console.ReadLine();

                int userChoice;
                bool valid = int.TryParse(input, out userChoice);

                // Check if the input is valid and correct
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

                    // End game if player runs out of lives
                    if (lives <= 0)
                    {
                        Console.WriteLine("\n💔 Game Over! You ran out of lives.");
                        break;
                    }
                }
            }

            // End game summary
            Console.WriteLine("\n=======================");
            Console.WriteLine($"🏁 Game over! Score: {score}/{questions.Count}");
            Console.WriteLine("=======================");

            // Show final score and update high score
            HandleHighScore(score, questions.Count);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        // Helper method to pause and clear the console screen
        static void NextQuestionEffect()
        {
            Console.WriteLine("\nPress Enter for the next question...");
            Console.ReadLine();
            Console.Clear();
        }

        // Helper method to read/update the high score and show feedback
        static void HandleHighScore(int score, int totalQuestions)
        {
            string highscoreFile = "highscore.txt";
            int topScore = 0;
        
        // Read existing high score from file if it exists
            if (File.Exists(highscoreFile))
            {
                string content = File.ReadAllText(highscoreFile);
                int.TryParse(content, out topScore);
            }

            Console.WriteLine($"🏆 High Score: {topScore}");
        
        // Save new high score if player beat the top score
            if (score > topScore)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("🌟 New high score! Congratulations!");
                Console.ResetColor();
                File.WriteAllText(highscoreFile, score.ToString());
            }
            Console.WriteLine("----------------------------");

            if (score == totalQuestions)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("👑 PERFECT SCORE! You are a Quiz Master!");
            }
            else if (score >= totalQuestions * 0.75)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("🌟 Excellent job! You really know your stuff!");
            }
            else if (score >= totalQuestions * 0.5)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("👍 Good effort! You passed!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("😅 Better luck next time! Keep practicing!");
            }
            Console.ResetColor();
            Console.WriteLine("-----------------------------");
        }
    }
}