using System;

class Entry
{
    public string Prompt;
    public string Response;
    public string Date; 
    public string Mood;
    public string Weather; 

    public Entry(string prompt, string response, string mood, string weather)
    {
        Prompt = prompt;
        Response = response;
        Mood = mood;
        Weather = weather;
        Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public override string ToString()
    {
        return $"{Date}: {Prompt}\nResponse: {Response}\nMood: {Mood}\nWeather: {Weather}\n";
    }
}

