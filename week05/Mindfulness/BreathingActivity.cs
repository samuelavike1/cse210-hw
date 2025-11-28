using System;

public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base(
            "Breathing",
            "This activity will help you relax by walking you through " +
            "breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public void Run()
    {
        DisplayStartingMessage();

        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        bool breatheIn = true;
        int cycles = 0;

        while (DateTime.Now < endTime)
        {
            if (breatheIn)
            {
                Console.Write("Breathe in... ");
            }
            else
            {
                Console.Write("Breathe out... ");
            }

            // 4-second breathing countdown
            ShowCountDown(4);
            breatheIn = !breatheIn;
            cycles++;
        }

        LogSession($"BreathCycles={cycles}");
        DisplayEndingMessage();
    }
}
