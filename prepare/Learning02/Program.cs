using System;
using System.Runtime.Serialization.Json;

class Program
{
    static void Main(string[] args)
    { //This is the storehouse that will assign the values to the properties and display it
        Job job1; //declare the variable to the class
        job1 = new Job (); //instantiating the variable
        // job1 is the instance(variable) that'll
        //store whatever value or memory is stored in the class used (class:Job)
        job1._jobTitle = "Software Engineer";
        job1._companyName = "Microsoft";
        job1._startYear = 2019;
        job1._endYear = 2022;
        // job1.Display();
        Job job2 = new Job (); //both ways to instantiate a class
        job2._jobTitle = "Manager";
        job2._companyName = "Apple";
        job2._startYear = 2022;
        job2._endYear = 2023;
        // job2.Display();
        Resume myResume = new Resume(); 
        //create the instance by using the constructor and the keyword new
        myResume._memberName = "Allison Parker";
        myResume._jobs.Add(job1);
        myResume._jobs.Add(job2);

        myResume.Display();
    }
}