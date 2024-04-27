using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("What is your first name? ");
        Console.WriteLine("What is your last name? ");

        string firstName = Console.ReadLine();
        string lastName = Console.ReadLine();

        string firstNameCapitalize = firstName.Substring(0,1).ToUpper() + firstName.Substring(1).ToLower();
        string lastNameCapitalize = lastName.Substring(0,1).ToUpper() + lastName.Substring(1).ToLower();

        Console.WriteLine($"Your name is {lastNameCapitalize}, {firstNameCapitalize}.");
        
    }
}