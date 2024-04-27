using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please enter your score: ");
        string scoreInput = Console.ReadLine();
        int ScoreIntVersion = int.Parse(scoreInput);
        
        string letter = ""; //to assign grade
        string sign = ""; //to assign sign
        
        if (ScoreIntVersion >= 90) // Determing grade
        {
            letter = "A";
        }
        else if (ScoreIntVersion >= 80)
        {
            letter = "B";
        }
        else if (ScoreIntVersion >=70)
        {
            letter = "C"; 
        }
        else if (ScoreIntVersion >=60)
        {
            letter = "D"; 
        }
        else
        {
            letter = "F"; 
        }
        
        if (ScoreIntVersion % 10 >= 7 ) //Determining '+' sign
        {
            
            sign = "+";
            
        }
        else if (ScoreIntVersion % 10 >= 3) //Determining '-' sign
        {
            sign = "-";
        }
        
        if (ScoreIntVersion >= 90) // Handle 'A' condition (!= A+/A-) & print statement
            {
               sign = letter;
               Console.Write($"Your grade is: {letter} ");
            }
        else if (ScoreIntVersion <60) // Handle 'F' conditon (!= F+/F-) & print statement
            {
               sign = letter;
               Console.Write($"Your grade is: {letter} ");
            }
        else
        {
            Console.Write($"Your grade is: {letter} {sign} ");
        }

        if (ScoreIntVersion >= 70) //Displaying 'pass' message
        {
            Console.WriteLine("Congratulation you passed!");
        }
        else
        {
            Console.Write("Sorry, Please try again"); //Displaying 'try again' message
        } 
    }
}