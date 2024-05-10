using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Program
{
    static void Main(string[] args)
    {
        ScriptureLibrary library = new ScriptureLibrary();
        library.LoadScripturesFromFile("scriptures.txt");
        library.AddScripture("Psalm 23:1", "The Lord is my shepherd; I shall not want.");
        library.DisplayScriptures();
        library.RemoveScripture("John 3:16");
        library.DisplayScriptures();
        library.DisplayRandomScripture();
        library.QuizMode();
    }
    public class Reference
    {
        public string ReferenceString { get; private set; }
        public Reference(string referenceStr)
        {
            ReferenceString = referenceStr;
            }
    // Other methods to handle single verses and verse ranges
    }
    public class Word
    {
        public string Text { get; private set; }
        public bool IsHidden { get; set; }
        public Word(string text)
        {
        Text = text;
        IsHidden = false;
        }
    }
    public class Scripture
    {
        private Reference reference;
        public Reference Reference { get { return reference; } }
        private List<Word> words;
        public Scripture(string referenceStr, string text)
        {
            reference = new Reference(referenceStr);
            words = text.Split(' ').Select(word => new Word(word)).ToList();
            }
        public void DisplayScripture()
        {
            Console.WriteLine($"Reference: {reference.ReferenceString}");
            Console.WriteLine("Text:");
            foreach (var word in words) //'var' is used to avoid repitition of function names
            {
                Console.Write(word.IsHidden ? "XXXX " : word.Text + " ");
            }
        Console.WriteLine();
        }
        public void HideRandomWords(int numWordsToHide)
        {
            Random rand = new Random();
            HashSet<int> indicesToHide = new HashSet<int>();
            while (indicesToHide.Count < numWordsToHide)
            {
                indicesToHide.Add(rand.Next(0, words.Count));
            }
            foreach (var index in indicesToHide)
            {
                words[index].IsHidden = true;
            }
        }
        public bool AllWordsHidden()
        {
            return words.All(word => word.IsHidden);
        }
        public List<Word> GetWords()
        {
            return words;
        }
        public void Run()
        {
            while (!AllWordsHidden())
            {
                Console.WriteLine("Press Enter to hide more words or type 'quit' to exit:");
                string input = Console.ReadLine();
                if (input.ToLower() == "quit")
                break;
                Console.Clear(); // Clear the console before displaying updated scripture
                Console.WriteLine("Enter the number of words to hide:");
                int numWordsToHide = Convert.ToInt32(Console.ReadLine());
                HideRandomWords(numWordsToHide);
                DisplayScripture();
            }
        }
    }
    public class ScriptureLibrary
    {
        private List<Scripture> scriptures;
        public ScriptureLibrary()
        {
            scriptures = new List<Scripture>();
        }
        public void LoadScripturesFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                return;
            }
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 2)
                {
                    scriptures.Add(new Scripture(parts[0], parts[1]));
                }
                else
                {
                    Console.WriteLine($"Invalid format in line: {line}");
                }
            }
        }
        public void DisplayRandomScripture()
        {
            Random rand = new Random();
            int index = rand.Next(0, scriptures.Count);
            scriptures[index].Run();
        }
        public void AddScripture(string referenceStr, string text)
        {
            scriptures.Add(new Scripture(referenceStr, text));
        }
        public void RemoveScripture(string referenceStr)
        {
            Scripture scriptureToRemove = scriptures.FirstOrDefault(s => s.Reference.ReferenceString == referenceStr);
            if (scriptureToRemove != null)
            {
                scriptures.Remove(scriptureToRemove);
            }
            else
            {
                Console.WriteLine("Scripture not found in the library.");
            }
        }
        public void DisplayScriptures()
        {
            Console.WriteLine("Scriptures in the library:");
            foreach (var scripture in scriptures)
            {
                Console.WriteLine(scripture.Reference.ReferenceString);
            }
        }
        // Additional features
        public void QuizMode()
        {
            Console.WriteLine("Welcome to Quiz Mode!");
            Console.WriteLine("Enter the scripture reference you want to practice:");
            string referenceStr = Console.ReadLine();
            Scripture selectedScripture = scriptures.FirstOrDefault(s => s.Reference.ReferenceString == referenceStr);
            if (selectedScripture != null)
            {
                selectedScripture.DisplayScripture();
                Console.WriteLine("Type 'start' when you're ready to begin the quiz.");
                string input = Console.ReadLine();
                if (input.ToLower() == "start")
                {
                    Console.Clear();
                    Console.WriteLine("Quiz started!");
                    Console.WriteLine("Type each word of the scripture as prompted:");
                    foreach (var word in selectedScripture.GetWords())
                    {
                        if (!word.IsHidden)
                        {
                            Console.Write($"{word.Text}: ");
                            string userResponse = Console.ReadLine();
                            if (userResponse.ToLower() != word.Text.ToLower())
                            {
                                Console.WriteLine("Incorrect. Try again.");
                                break;
                            }
                        }
                    }
                Console.WriteLine("Congratulations! You've completed the quiz.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Quiz aborted.");
                }
            }
            else
            {
                Console.WriteLine("Scripture not found in the library.");
            }
        }
    }
}
