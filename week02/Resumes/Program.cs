using System;

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job();
        job1._jobTitle = "ML Engineer";
        job1._company = "Hubtel";
        job1._startYear = 2021;
        job1._endYear = 2023;

        Job job2 = new Job();
        job2._jobTitle = "Technical Lead";
        job2._company = "TeleSoft";
        job2._startYear = 2023;
        job2._endYear = 2025;

        Job job3 = new Job();
        job3._jobTitle = "IT Specialist";
        job3._company = "V-NET";
        job3._startYear = 2017;
        job3._endYear = 2020;

        Resume myResume = new Resume();
        myResume._name = "Samuel Avike";

        myResume._jobs.Add(job1);
        myResume._jobs.Add(job2);
        myResume._jobs.Add(job3);

        myResume.Display();
    }
}