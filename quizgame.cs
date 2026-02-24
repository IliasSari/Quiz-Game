using System;

class quizgame
{
    static void Main()
    {
        Console.WriteLine("Quiz game");

        Console.WriteLine("Question");
        Console.WriteLine("What is the Capital of Greece?");

        Console.Write("Answer");
        string answer = Console.ReadLine();

        if (answer.ToLower() == "athens")
        {
            Console.WriteLine("Correct!");
        }
        else
        {
            Console.WriteLine("Wrong! The correct answer is Athens!");
        }
    }
}

