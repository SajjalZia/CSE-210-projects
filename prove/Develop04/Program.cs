using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Mindfulness App!");
        Console.WriteLine("Choose an activity:");
        Console.WriteLine("1. Breathing Activity");
        Console.WriteLine("2. Reflection Activity");
        Console.WriteLine("3. Listing Activity");

        int choice;
        do
        {
            Console.Write("Enter your choice (1-3): ");
            choice = Convert.ToInt32(Console.ReadLine());
        } while (choice < 1 || choice > 3);

        int duration;
        Console.Write("Enter duration (in seconds): ");
        duration = Convert.ToInt32(Console.ReadLine());

        MindfulnessActivity activity = null;
        switch (choice)
        {
            case 1:
                activity = new BreathingActivity(duration);
                break;
            case 2:
                activity = new ReflectionActivity(duration);
                break;
            case 3:
                activity = new ListingActivity(duration);
                break;
        }

        if (activity != null)
        {
            activity.StartActivity();
            activity.PerformActivity();
            activity.EndActivity();
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }

    abstract class MindfulnessActivity // Base class for all activities
    {
        protected int durationInSeconds;
        protected List<string> usedPrompts = new List<string>(); // For storing used prompts/questions
        protected static Random rand = new Random();

        public MindfulnessActivity(int duration)
        {
            durationInSeconds = duration;
        }

        public virtual void StartActivity() // Common starting message for all activities
        {
            Console.WriteLine("Starting " + GetType().Name + " activity...");
            Thread.Sleep(2000); // Pause for 2 seconds
            Console.WriteLine("Get ready to begin...");
            Thread.Sleep(2000); // Pause for 2 seconds
        }

        public virtual void EndActivity() // Common ending message for all activities
        {
            Console.WriteLine("Good job! You have completed the " + GetType().Name + " activity.");
            Console.WriteLine("Duration: " + durationInSeconds + " seconds");
            Thread.Sleep(2000); // Pause for 2 seconds
            Console.WriteLine("Activity completed.");
            Thread.Sleep(2000); // Pause for 2 seconds
        }

        protected void DisplayAnimation(int seconds) // Method to display animation during pauses
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(".");
                Thread.Sleep(1000); // Pause for 1 second
            }
            Console.WriteLine();
        }

        protected void LogActivity() // Log activity details to a text file
        {
            string logEntry = $"{DateTime.Now} - {GetType().Name} activity, Duration: {durationInSeconds} seconds";
            File.AppendAllText("activity_log.txt", logEntry + Environment.NewLine);
        }

        protected string GetUniquePrompt(List<string> prompts) // Get a unique random prompt/question
        {
            if (prompts.Count == 0)
                return null;

            int index = rand.Next(prompts.Count);
            string prompt = prompts[index];
            prompts.RemoveAt(index);
            return prompt;
        }

        public abstract void PerformActivity(); // Abstract method to perform the activity
    }

    class BreathingActivity : MindfulnessActivity // Breathing activity class
    {
        public BreathingActivity(int duration) : base(duration)
        {
        }

        public override void StartActivity()
        {
            base.StartActivity();
            Console.WriteLine("This activity will help you relax by walking you through breathing in and out slowly.");
            Console.WriteLine("Clear your mind and focus on your breathing.");
        }

        public override void PerformActivity()
        {
            Console.WriteLine("Starting breathing activity...");
            for (int i = 0; i < durationInSeconds; i += 2)
            {
                Console.WriteLine("Breathe in...");
                DisplayAnimation(2); // Pause for 2 seconds
                Console.WriteLine("Breathe out...");
                DisplayAnimation(2); // Pause for 2 seconds
            }
            LogActivity();
        }
    }

    class ReflectionActivity : MindfulnessActivity // Reflection activity class
    {
        private List<string> prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        public ReflectionActivity(int duration) : base(duration)
        {
        }

        public override void StartActivity()
        {
            base.StartActivity();
            Console.WriteLine("This activity will help you reflect on times in your life when you have shown strength and resilience.");
            Console.WriteLine("This will help you recognize the power you have and how you can use it in other aspects of your life.");
        }

        public override void PerformActivity()
        {
            Console.WriteLine("Starting reflection activity...");
            List<string> remainingPrompts = new List<string>(prompts);
            List<string> remainingQuestions = new List<string>(questions);

            for (int i = 0; i < durationInSeconds; i += 10) // Assuming each question takes 10 seconds
            {
                if (remainingPrompts.Count == 0)
                    remainingPrompts = new List<string>(prompts);

                string prompt = GetUniquePrompt(remainingPrompts);
                Console.WriteLine("Prompt: " + prompt);
                DisplayAnimation(2); // Pause for 2 seconds

                foreach (string question in remainingQuestions)
                {
                    Console.WriteLine("Question: " + question);
                    DisplayAnimation(5); // Pause for 5 seconds
                }
            }
            LogActivity();
        }
    }

    class ListingActivity : MindfulnessActivity // Listing activity class
    {
        private List<string> prompts = new List<string>
        {
            "Who are the people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are the people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity(int duration) : base(duration)
        {
        }

        public override void StartActivity()
        {
            base.StartActivity();
            Console.WriteLine("This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
        }

        public override void PerformActivity()
        {
            Console.WriteLine("Starting listing activity...");
            List<string> remainingPrompts = new List<string>(prompts);

            if (remainingPrompts.Count == 0)
                remainingPrompts = new List<string>(prompts);

            string prompt = GetUniquePrompt(remainingPrompts);
            Console.WriteLine("Prompt: " + prompt);
            DisplayAnimation(5); // Pause for 5 seconds
            Console.WriteLine("Start listing items...");
            Thread.Sleep(durationInSeconds * 1000); // Pause for duration specified by user
            Console.WriteLine("End listing items.");
            LogActivity();
        }
    }
}
