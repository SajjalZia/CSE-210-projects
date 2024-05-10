using System;

class Program
{
    static void Main(string[] args)
    {
        // Private attributes for the numerator and denominator
        private int numerator;
        private int denominator;

        // Constructors
        public Fraction()
        {
            numerator = 1;
            denominator = 1;
        }

        public Fraction(int top)
        {
            numerator = top;
            denominator = 1;
        }

        public Fraction(int top, int bottom)
        {
            numerator = top;
            denominator = bottom;
        }

        // Getters and setters
        public int GetNumerator()
        {
            return numerator;
        }

        public void SetNumerator(int num)
        {
            numerator = num;
        }

        public int GetDenominator()
        {
            return denominator;
        }

        public void SetDenominator(int denom)
        {
            if (denom != 0) // Check for division by zero
                denominator = denom;
            else
                Console.WriteLine("Error: Denominator cannot be zero.");
        }

        // Method to return fraction as a string
        public string GetFractionString()
        {
            return numerator + "/" + denominator;
        }

        // Method to return decimal value of the fraction
        public double GetDecimalValue()
        {
            return (double)numerator / denominator;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Testing the Fraction class
            Fraction fraction1 = new Fraction(); // 1/1
            Fraction fraction2 = new Fraction(5); // 5/1
            Fraction fraction3 = new Fraction(3, 4); // 3/4
            Fraction fraction4 = new Fraction(1, 3); // 1/3

            // Displaying fractions
            Console.WriteLine(fraction1.GetFractionString());
            Console.WriteLine(fraction1.GetDecimalValue());

            Console.WriteLine(fraction2.GetFractionString());
            Console.WriteLine(fraction2.GetDecimalValue());

            Console.WriteLine(fraction3.GetFractionString());
            Console.WriteLine(fraction3.GetDecimalValue());

            Console.WriteLine(fraction4.GetFractionString());
            Console.WriteLine(fraction4.GetDecimalValue());
        }
    }
}