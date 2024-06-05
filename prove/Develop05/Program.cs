using System;
using System.Collections.Generic;
using System.IO;

static class Program
{
    static List<GoalActivity> goals = new List<GoalActivity>();
    static int totalPoints = 0;
    static int streak = 0;
    static int level = 1;

    static void Main(string[] args)
    {
        LoadGoals(); // Load saved goals from file

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nEternal Quest - Goals Tracker");
            Console.WriteLine($"Score: {totalPoints} | Streak: {streak} | Level: {level}");
            Console.WriteLine("1. Add New Goal");
            Console.WriteLine("2. Record Goal Achievement");
            Console.WriteLine("3. View Goals");
            Console.WriteLine("4. Save and Exit");

            Console.Write("Enter your choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        AddGoal();
                        break;
                    case 2:
                        RecordGoalAchievement();
                        break;
                    case 3:
                        ViewGoals();
                        break;
                    case 4:
                        SaveGoals();
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }

    static void AddGoal()
    {
        Console.WriteLine("\nAdding a New Goal:");

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();

        Console.Write("Enter points for completing this goal: ");
        if (!int.TryParse(Console.ReadLine(), out int points))
        {
            Console.WriteLine("Invalid points. Goal not added.");
            return;
        }

        Console.WriteLine("Select goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.WriteLine("4. Large Goal");

        Console.Write("Enter your choice: ");
        if (!int.TryParse(Console.ReadLine(), out int typeChoice))
        {
            Console.WriteLine("Invalid choice. Goal not added.");
            return;
        }

        switch (typeChoice)
        {
            case 1:
                goals.Add(new SimpleGoalActivity(name, points));
                break;
            case 2:
                goals.Add(new EternalGoalActivity(name, points));
                break;
            case 3:
                Console.Write("Enter number of times to complete the goal: ");
                if (!int.TryParse(Console.ReadLine(), out int requiredTimes))
                {
                    Console.WriteLine("Invalid input. Goal not added.");
                    return;
                }
                Console.Write("Enter bonus points for completing all times: ");
                if (!int.TryParse(Console.ReadLine(), out int bonusPoints))
                {
                    Console.WriteLine("Invalid input. Goal not added.");
                    return;
                }
                goals.Add(new ChecklistGoalActivity(name, points, requiredTimes, bonusPoints));
                break;
            case 4:
                Console.Write("Enter the target progress for the goal: ");
                if (!int.TryParse(Console.ReadLine(), out int target))
                {
                    Console.WriteLine("Invalid input. Goal not added.");
                    return;
                }
                goals.Add(new LargeGoalActivity(name, points, target));
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void RecordGoalAchievement()
    {
        Console.WriteLine("\nRecording Goal Achievement:");
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals currently exist.");
            return;
        }

        Console.WriteLine("Select a goal to mark as complete:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetStatus()} - {goals[i].Name}");
        }

        Console.Write("Enter your choice: ");
        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            if (choice >= 1 && choice <= goals.Count)
            {
                goals[choice - 1].MarkComplete(ref totalPoints, ref streak, ref level);
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }

    static void ViewGoals()
    {
        Console.WriteLine("\nCurrent Goals:");
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals currently exist.");
            return;
        }

        foreach (var goal in goals)
        {
            Console.WriteLine($"{goal.GetStatus()} - {goal.Name}");
        }
    }

    static void SaveGoals()
    {
        using (StreamWriter outputFile = new StreamWriter("goals.txt"))
        {
            outputFile.WriteLine($"{totalPoints},{streak},{level}");
            foreach (var goal in goals)
            {
                switch (goal)
                {
                    case SimpleGoalActivity simpleGoal:
                        outputFile.WriteLine($"SimpleGoalActivity,{goal.Name},{goal.Points},{simpleGoal.Completed}");
                        break;
                    case EternalGoalActivity:
                        outputFile.WriteLine($"EternalGoalActivity,{goal.Name},{goal.Points}");
                        break;
                    case ChecklistGoalActivity checklistGoal:
                        outputFile.WriteLine($"ChecklistGoalActivity,{goal.Name},{goal.Points},{checklistGoal.RequiredTimes},{checklistGoal.BonusPoints},{checklistGoal.CompletedTimes}");
                        break;
                    case LargeGoalActivity largeGoal:
                        outputFile.WriteLine($"LargeGoalActivity,{goal.Name},{goal.Points},{largeGoal.Target},{largeGoal.Progress}");
                        break;
                }
            }
        }
        Console.WriteLine("Goals saved successfully.");
    }

    static void LoadGoals()
    {
        goals.Clear();
        try
        {
            string[] lines = File.ReadAllLines("goals.txt");
            string[] header = lines[0].Split(',');
            totalPoints = int.Parse(header[0]);
            streak = int.Parse(header[1]);
            level = int.Parse(header[2]);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                string type = parts[0];
                string name = parts[1];
                int points = int.Parse(parts[2]);

                switch (type)
                {
                    case "SimpleGoalActivity":
                        bool completed = bool.Parse(parts[3]);
                        var simpleGoal = new SimpleGoalActivity(name, points)
                        {
                            Completed = completed
                        };
                        goals.Add(simpleGoal);
                        break;
                    case "EternalGoalActivity":
                        goals.Add(new EternalGoalActivity(name, points));
                        break;
                    case "ChecklistGoalActivity":
                        int requiredTimes = int.Parse(parts[3]);
                        int bonusPoints = int.Parse(parts[4]);
                        int completedTimes = int.Parse(parts[5]);
                        var checklistGoal = new ChecklistGoalActivity(name, points, requiredTimes, bonusPoints)
                        {
                            CompletedTimes = completedTimes
                        };
                        goals.Add(checklistGoal);
                        break;
                    case "LargeGoalActivity":
                        int target = int.Parse(parts[3]);
                        int progress = int.Parse(parts[4]);
                        var largeGoal = new LargeGoalActivity(name, points, target)
                        {
                            Progress = progress
                        };
                        goals.Add(largeGoal);
                        break;
                    default:
                        Console.WriteLine($"Unknown goal type '{type}' found in file.");
                        break;
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("No saved goals found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading goals: {ex.Message}");
        }
    }

    public static void LevelUp(ref int level, ref int totalPoints)
    {
        int nextLevelThreshold = level * 100;
        if (totalPoints >= nextLevelThreshold)
        {
            level++;
            totalPoints -= nextLevelThreshold;
            Console.WriteLine($"Congratulations! You've reached level {level}!");
        }
    }
}

// Base class for all goal activities
abstract class GoalActivity
{
    public string Name { get; private set; }
    public int Points { get; private set; }

    public GoalActivity(string name, int points)
    {
        Name = name;
        Points = points;
    }

    public abstract void MarkComplete(ref int totalPoints, ref int streak, ref int level);
    public abstract string GetStatus();
}

// Derived class for simple goals
class SimpleGoalActivity : GoalActivity
{
    public bool Completed { get; set; }

    public SimpleGoalActivity(string name, int points) : base(name, points)
    {
        Completed = false;
    }

    public override void MarkComplete(ref int totalPoints, ref int streak, ref int level)
    {
        if (!Completed)
        {
            Completed = true;
            totalPoints += Points;
            streak++;
            Program.LevelUp(ref level, ref totalPoints);
            Console.WriteLine($"Goal '{Name}' completed! +{Points} points.");
        }
        else
        {
            Console.WriteLine($"Goal '{Name}' is already completed.");
        }
    }

    public override string GetStatus()
    {
        return Completed ? "[X]" : "[ ]";
    }
}

// Derived class for eternal goals
class EternalGoalActivity : GoalActivity
{
    public EternalGoalActivity(string name, int points) : base(name, points)
    {
    }

    public override void MarkComplete(ref int totalPoints, ref int streak, ref int level)
    {
        totalPoints += Points;
        streak++;
        Program.LevelUp(ref level, ref totalPoints);
        Console.WriteLine($"Eternal goal '{Name}' recorded! +{Points} points.");
    }

    public override string GetStatus()
    {
        return "[E]";
    }
}

// Derived class for checklist goals
class ChecklistGoalActivity : GoalActivity
{
    public int CompletedTimes { get; set; }
    public int RequiredTimes { get; private set; }
    public int BonusPoints { get; private set; }

    public ChecklistGoalActivity(string name, int points, int requiredTimes, int bonusPoints) : base(name, points)
    {
        RequiredTimes = requiredTimes;
        BonusPoints = bonusPoints;
        CompletedTimes = 0;
    }

    public override void MarkComplete(ref int totalPoints, ref int streak, ref int level)
    {
        CompletedTimes++;
        totalPoints += Points;
        streak++;
        Program.LevelUp(ref level, ref totalPoints);
        Console.WriteLine($"Goal '{Name}' recorded! +{Points} points.");

        if (CompletedTimes == RequiredTimes)
        {
            totalPoints += BonusPoints;
            Console.WriteLine($"Bonus achieved for completing {RequiredTimes} times! +{BonusPoints} points.");
            Program.LevelUp(ref level, ref totalPoints);
        }
    }

    public override string GetStatus()
    {
        return $"Completed {CompletedTimes}/{RequiredTimes} times";
    }
}

// Derived class for large goals
class LargeGoalActivity : GoalActivity
{
    public int Progress { get; set; }
    public int Target { get; private set; }

    public LargeGoalActivity(string name, int points, int target) : base(name, points)
    {
        Target = target;
        Progress = 0;
    }

    public override void MarkComplete(ref int totalPoints, ref int streak, ref int level)
    {
        Progress++;
        totalPoints += Points;
        streak++;
        Program.LevelUp(ref level, ref totalPoints);
        Console.WriteLine($"Progress made on large goal '{Name}'! +{Points} points.");

        if (Progress == Target)
        {
            Console.WriteLine($"Large goal '{Name}' completed!");
        }
    }

    public override string GetStatus()
    {
        return $"Progress {Progress}/{Target}";
    }
}
