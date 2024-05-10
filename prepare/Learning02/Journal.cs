using System;

public class Journal
{
    public List<Entry> _entries;
    public void AddEntry(Entry newEntry)
    {
       _entries.Add(newEntry);
    }
    public void DisplayAll()
    {
         foreach (var entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string file)
    {
        using (StreamWriter writer = new StreamWriter(file))
        {
            foreach (var entry in _entries)
            {
                writer.WriteLine($"{entry._date},{entry._promptText},{entry._entryText},{entry._mood},{entry._location}");
            }
        }
    }
    public void LoadFromFile(string file)
    {
         _entries.Clear();
        using (StreamReader reader = new StreamReader(file))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                Entry newEntry = new Entry
                {
                    _date = parts[0],
                    _promptText = parts[1],
                    _entryText = parts[2],
                    _mood = parts[3],
                    _location = parts[4]
                };
                _entries.Add(newEntry);
            }
        }
    }

     public void SaveToJson(string file)
    {
        string json = JsonConvert.SerializeObject(_entries, Formatting.Indented);
        File.WriteAllText(file, json);
    }

    public void LoadFromJson(string file)
    {
        string json = File.ReadAllText(file);
        _entries = JsonConvert.DeserializeObject<List<Entry>>(json);
    }
}