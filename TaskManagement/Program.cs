TaskDB db = new TaskDB("TasksDataBase.sqlite");
db.Connect();
db.CreateTasksTable();

TaskManager taskManager = new TaskManager();
Menu menu = new Menu(taskManager);

do
{   
    Console.WriteLine(menu.ToString());
    try
    {
        menu.ReadChoice();
        menu.ExecuteChoice();
    }
    catch(FormatException e)
    {
        Console.WriteLine(e.Message + " : Please write a number.");
    }
    catch(OverflowException e)
    {
        Console.WriteLine(e.Message + " : Please write a small number.");
    }

} while (menu.Status);




