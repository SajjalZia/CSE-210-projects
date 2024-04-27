using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        int userNumber = -1;
        while (userNumber != 0)
        {
            Console.Write("Please enter a number (0 if want to leave): ");
            
            string userInput = Console.ReadLine();
            userNumber = int.Parse(userInput);
            if (userNumber != 0)
            {
                numbers.Add(userNumber);
            }
        }
        int sumNumber = 0;
        foreach (int number in numbers)
        {
            sumNumber += number;
        }

        Console.WriteLine($"Sum: {sumNumber}");

        float averageNumber = ((float)sumNumber) / numbers.Count;
        Console.WriteLine($"Average: {averageNumber}");
        
        int maxNumber = numbers[0];

        foreach (int number in numbers)
        {
            if (number > maxNumber)
            {
                maxNumber = number;
            }
        }

        Console.WriteLine($"Highest Value: {maxNumber}");
    }
}