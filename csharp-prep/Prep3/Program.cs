using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        int magicNumber = randomGenerator.Next(1, 101);
        int guess = 0;
        while (guess != magicNumber)
        {
            Console.WriteLine("What is your guess? "); //guess input
            guess = int.Parse(Console.ReadLine());
            if (guess < magicNumber)
            {
                Console.Write("Please look for a higher number ");
             }
             else if (guess > magicNumber)
             {
                Console.Write("Please look for a lower number ");
                }
                else 
                {
                    Console.Write("Yay! you have guessed the number ");
                }
            }
        }
}