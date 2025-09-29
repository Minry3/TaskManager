using System;
using System.Runtime.CompilerServices;
using TaskManagement.UpdateTypes;

public class Menu	
{
	private TaskManager taskManager;
	private bool status = true;
	private TypeChoice choice;

    public bool Status { get => status ; set => status = value; }
    public TypeChoice Choice { get => choice; set => choice = value; }


    public Menu(TaskManager aTaskManager)
	{
		this.taskManager = aTaskManager;
	}

    public void Quit()
	{
		Console.WriteLine("\nSee you next time :)");
		this.Status = false;
		Thread.Sleep(1000);
		Console.WriteLine("\nBye bye!");
		Thread.Sleep(500);
	}

    public void ReadChoice()
	{
        Console.Write("Your choice : ");
		this.Choice = (TypeChoice)int.Parse(Console.ReadLine().Trim());
	}

	public enum TypeChoice 
	{
		Add=1,
		Delete,
		Update,
		Print,
		Quit
	}


    public void ExecuteChoice()
	{
		switch(this.choice)
		{
			case TypeChoice.Add:
				this.taskManager.AddTask();
				break;
			case TypeChoice.Delete:
				this.taskManager.DeleteTask();
				break;
			case TypeChoice.Update:
				TypeUpdate updateChoice = ReadTypeUpdate();
				this.taskManager.UpdateTask(updateChoice);
				break;
			case TypeChoice.Print:
				this.taskManager.PrintTasks();
				break;
			case TypeChoice.Quit:
				Quit();
				break;
			default:
				Console.WriteLine("This choice is not valid. Please choose a valid option.");
				break;
		}
	}

	public TypeUpdate ReadTypeUpdate()
	{
        Console.WriteLine("\n======  Update a task  =======\n");
        Console.WriteLine($"What do you want to do ?\n\n   " +
                          $"{(int)TypeUpdate.Name}. Change the name\n   " +
                          $"{(int)TypeUpdate.Description}. Change the description\n");
        Console.Write("\nYour choice : ");
        TypeUpdate choice = (TypeUpdate)int.Parse(Console.ReadLine().Trim());
		return choice;
    }

    public string ToString()
    {
        return $"""
			
			============ MENU ============

			Please choose an option
			
				{(int)TypeChoice.Add}. Add a new task
				{(int)TypeChoice.Delete}. Delete a task
				{(int)TypeChoice.Update}. Update a task
				{(int)TypeChoice.Print}. Show all tasks
				{(int)TypeChoice.Quit}. Quit

			==============================
			""";
    }
}
