using System;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // ----- Video 1 -----
        Video video1 = new Video("Intro to Abstraction", "Professor Smith", 420);
        video1.AddComment(new Comment("Alice", "This helped me understand abstraction!"));
        video1.AddComment(new Comment("Bob", "Very clear explanation, thanks."));
        video1.AddComment(new Comment("Charlie", "Can you do one on encapsulation next?"));
        videos.Add(video1);

        // ----- Video 2 -----
        Video video2 = new Video("C# Classes and Objects", "CodeAcademy", 615);
        video2.AddComment(new Comment("Diana", "Exactly what I needed for my homework."));
        video2.AddComment(new Comment("Ethan", "Love the examples."));
        video2.AddComment(new Comment("Fiona", "Subscribed!"));
        videos.Add(video2);

        // ----- Video 3 -----
        Video video3 = new Video("LINQ in 10 Minutes", "Dev Channel", 590);
        video3.AddComment(new Comment("George", "LINQ always confused me, this helps."));
        video3.AddComment(new Comment("Hannah", "Please make more LINQ videos."));
        video3.AddComment(new Comment("Ian", "The demo at the end was great."));
        videos.Add(video3);

        // Display all videos and their comments
        foreach (Video video in videos)
        {
            video.DisplayFullDetails();
            Console.WriteLine(new string('-', 50));
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}