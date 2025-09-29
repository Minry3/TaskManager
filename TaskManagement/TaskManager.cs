using Microsoft.VisualBasic.FileIO;
using System;
using System.Diagnostics.Metrics;
using System.Text;
using System.Xml.Linq;
using TaskManagement.UpdateTypes;
using static Menu;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class TaskManager
{
    private TaskDB db = new TaskDB("TasksDataBase.sqlite");

    public TaskManager()
    {
    }

    public void AddTask()
    {
        Console.WriteLine("\n========= Add a task =========\n");

        Console.Write("Name : ");
        string name = AskName();
        Console.Write("Description (if it doesn't have a description press Enter) : ");
        string description = Console.ReadLine().ToLower();

        Task task = new Task(name, description);

        if (db.InsertTask(task) != null)
            Console.WriteLine("\n\n     Task added succesfully!\n");
        else
            Console.WriteLine("\n\n     The task could not be added :(\n");
    }

    public void UpdateTask(TypeUpdate choice)
    {
        int id;
        bool resultUpdate = false;
        switch (choice)
        {
            case TypeUpdate.Name:
                id = AskId("update");
                Console.Write("New name : ");
                string name = AskName();
                resultUpdate = db.UpdateName(id, name);
                break;
            case TypeUpdate.Description:
                id = AskId("update");
                Console.Write("New description : ");
                string description = Console.ReadLine().ToLower();
                resultUpdate = db.UpdateDescription(id, description);
                break;
            default:
                Console.WriteLine("This choice is not valid.Please choose a valid option.");
                return;
        }
        if (resultUpdate)
            Console.WriteLine("\n\n     Task updated!");
        else
            Console.WriteLine(" The task could not be updated :(");
    }

    public int AskId(string operation)
    {
        Console.WriteLine($"Please write the ID of the task you want to {operation}");
        Console.Write("ID : ");
        int id = int.Parse(Console.ReadLine().Trim());
        return id;
    }

    public void DeleteTask()
    {
        Console.WriteLine("\n======  Delete a task  =======\n");

        int id = AskId("delete");

        if (db.DeleteTask(id))
            Console.WriteLine("\n\n     Task deleted succesfully! \n");
        else
            Console.WriteLine("\n\nThe task could not be removed :( \n      Please verify the ID\n");
    }

    public void PrintTasks()
    {
        Console.WriteLine("\n\n========= Tasks List =========\n");
        List<Task> tasks = db.GetAllTasks();
        if (tasks.Count == 0)
            Console.WriteLine("         List empty");
        else
        {
            foreach (Task task in tasks)
            {
                Console.WriteLine(task.ToString());
            }
        }
    }

    public string AskName()
    {
        string name = Console.ReadLine().ToLower();
        while (name == string.Empty)
        {
            Console.Write("\n !WARNING! : A name is needed\n");
            Console.Write("Name : ");
            name = Console.ReadLine().ToLower();
        }
        return name;
    }

    //filter pour le print

}
