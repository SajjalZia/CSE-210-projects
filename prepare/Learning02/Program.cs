using System;
class Program
{
    static void Main(string[] args)
    { 
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        // Sample usage: Generate entry with random prompt and display
        Entry newEntry = new Entry
        {
            _date = DateTime.Now.ToString("yyyy-MM-dd"),
            _promptText = promptGenerator.GetRandomPrompt(),
            _entryText = "Sample entry text",
            _mood = "Happy",
            _location = "Home"
        };

        journal.AddEntry(newEntry);
        journal.DisplayAll();
    }
}