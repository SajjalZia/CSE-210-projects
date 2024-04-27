using System;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcomeMessage();

        string nameInput = PromptUserName();
        int numberInput = PromptUserNumber();

        int numberSquare = SquareNumber(numberInput);

        DisplayResult(nameInput, numberSquare);
    }

    static void DisplayWelcomeMessage()
    {
        Console.WriteLine("Hello, Welcome to the program!");
    }

    static string PromptUserName()
    {
        Console.Write("Please enter your full name: ");
        string userName = Console.ReadLine();       
        return userName;
    }

    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        int number = int.Parse(Console.ReadLine());

        return number;
    }

    static int SquareNumber(int number)
    {
        int square = number * number;
        return square;
    }

    static void DisplayResult(string name, int square)
    {
        Console.WriteLine($"{name}, the square of your number is {square}");
    }
}