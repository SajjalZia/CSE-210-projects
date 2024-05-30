using System;
using System.Collections.Generic;

// Define the Comment class
class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}

// Define the Video class
class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; } // Length in seconds
    private List<Comment> comments;

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return comments.Count;
    }

    public List<Comment> GetComments()
    {
        return comments;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create 3-4 videos
        Video video1 = new Video("Understanding Polymorphism", "Tech Academy", 600);
        Video video2 = new Video("Abstract Classes in C#", "Coding Guru", 750);
        Video video3 = new Video("Inheritance in OOP", "Developer Hub", 480);

        // Add 3-4 comments for each video
        video1.AddComment(new Comment("Alice", "Great explanation!"));
        video1.AddComment(new Comment("Bob", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Charlie", "I finally understand this topic."));

        video2.AddComment(new Comment("Dave", "Awesome tutorial!"));
        video2.AddComment(new Comment("Eve", "Clear and concise."));
        video2.AddComment(new Comment("Frank", "Thanks for the detailed video."));

        video3.AddComment(new Comment("Grace", "This was so informative."));
        video3.AddComment(new Comment("Heidi", "Excellent presentation."));
        video3.AddComment(new Comment("Ivan", "Really good examples."));

        // Add videos to a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display information for each video and its comments
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of comments: {video.GetNumberOfComments()}");

            Console.WriteLine("Comments:");
            foreach (var comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.Name}: {comment.Text}");
            }
            Console.WriteLine();
        }
    }
}
