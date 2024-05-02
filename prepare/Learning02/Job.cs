    public class Job
    { //This is the storage box/a memory with all the properties
        public string _companyName;
        public string _jobTitle;
        public int _startYear;
        public int _endYear;
        public void Display() 
        //the function will be used when you call to
        //display the properties you want to display

        {
            Console.Write($"{_jobTitle} ({_companyName}) {_startYear}-{_endYear}"); 
            // add the properties you want to display
        }   
    }