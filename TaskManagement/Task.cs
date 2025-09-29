using System;

public class Task
{
    private int id;
	private string name;
	private string description;
	private string date;
    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public string Date { get => date; set => date = value; }

    public Task(string aName, string aDescription)
	{
		this.name = aName;
		this.description = aDescription;
        this.date = DateTime.Now.ToString("yyyy-MM-dd");
    }

    public Task(int anId, string aName, string aDescription, string aDate)
    {
        this.id = anId;
        this.name = aName;
        this.description = aDescription;
        this.date = aDate;
    }
    
    public string ToString()
    {
        return $"[{this.id}] {this.name.ToUpper()} ({this.date}) \n ---> {this.description}\n";
    }
}
