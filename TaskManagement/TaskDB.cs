using System;
using System.Data;
using System.Data.SQLite;
using System.Runtime.CompilerServices;

public class TaskDB
{

    private string dataSrc;

    private string connectionString;

    public TaskDB(string theDataSrc)
    {
        this.dataSrc = theDataSrc;
        this.connectionString = "Data Source=" + this.dataSrc + ";Version=3;";
    }
    public void Connect()
    {
        if (!File.Exists(this.dataSrc))
        {
            SQLiteConnection.CreateFile(this.dataSrc);
            Console.WriteLine("DataBase created!");
        }

        using SQLiteConnection connection = new(this.connectionString);

        connection.Open();

        if (connection.State == System.Data.ConnectionState.Open)
            Console.WriteLine("Connection established!");
        else
            Console.WriteLine("The connection has failed :(");
    }


    public bool CreateTasksTable()
    {
        using SQLiteConnection connection = new(this.connectionString);

        connection.Open();

        string request = @"
				CREATE TABLE IF NOT EXISTS Tasks (
					Id INTEGER PRIMARY KEY AUTOINCREMENT,
					Name TEXT NOT NULL,
					Description TEXT,
					Date TEXT NOT NULL
				);
			";

        using SQLiteCommand command = new(request, connection);

        return command.ExecuteNonQuery() == 1;
    }
    public bool DeleteTask(int id)
    {
        using SQLiteConnection connection = new(this.connectionString);

        connection.Open();

        string request = @"
				DELETE FROM Tasks WHERE Id = @id;
			";

        using SQLiteCommand command = new(request, connection);

        command.Parameters.AddWithValue("@id", id);

        return command.ExecuteNonQuery() == 1;
    }

    public Task InsertTask(Task task)
    {
        using SQLiteConnection connection = new(this.connectionString);

        connection.Open();

        string request = @"
				INSERT INTO Tasks (Name, Description, Date) VALUES (@name, @description, @date);
			";

        using SQLiteCommand command = new(request, connection);

        command.Parameters.AddWithValue("@name", task.Name);
        command.Parameters.AddWithValue("@description", task.Description);
        command.Parameters.AddWithValue("@date", task.Date);

        if (command.ExecuteNonQuery() == 1)
            return new Task(GetLastId(), task.Name, task.Description, task.Date);
        return null;
    }

    private int GetLastId()
    {
        using SQLiteConnection connection = new(this.connectionString);
        connection.Open();
        string request = @"
            SELECT Id FROM Tasks ORDER BY Id DESC LIMIT 1;
        ";
        using SQLiteCommand command = new(request, connection);
        using SQLiteDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            return Convert.ToInt32(reader["Id"]);
        }
        return 0;
    }

    public Task SelectById(int id)
    {
        using SQLiteConnection connection = new(this.connectionString);

        connection.Open();

        string request = @"
				SELECT Name, Description, Date FROM Tasks WHERE Id = @id;
			";

        using SQLiteCommand command = new(request, connection);

        command.Parameters.AddWithValue("@id", id);

        using SQLiteDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            string name = reader["Name"].ToString();
            string description = reader["Description"] == DBNull.Value ? String.Empty : reader["Description"].ToString();
            string date = reader["Date"].ToString();

            return new Task(id, name, description, date);
        }
        else
            throw new TaskException("There is no task with this id");
    }

    public List<Task> GetAllTasks()
    {
        List<Task> tasks = new();

        using SQLiteConnection connection = new(this.connectionString);
        connection.Open();
        string request = @"
			SELECT Id FROM Tasks;
		";
        using SQLiteCommand command = new(request, connection);
        using SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            int id = Convert.ToInt32(reader["Id"]);
            Task task = SelectById(id);
            tasks.Add(task);
        }
        return tasks;
    }

    public bool UpdateName(int id, string name)
    {
        using SQLiteConnection connection = new(this.connectionString);
        connection.Open();
        string request = @"
            UPDATE Tasks
            SET Name = @name
            WHERE Id = @id;
        ";
        using SQLiteCommand command = new(request, connection);
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@id", id);

        return command.ExecuteNonQuery() == 1;
    }

    public bool UpdateDescription(int id, string description)
    {
        using SQLiteConnection connection = new(this.connectionString);
        connection.Open();
        string request = @"
            UPDATE Tasks
            SET Description = @description
            WHERE Id = @id;
        ";
        using SQLiteCommand command = new(request, connection);
        command.Parameters.AddWithValue("@description", description);
        command.Parameters.AddWithValue("@id", id);

        return command.ExecuteNonQuery() == 1;
    }
}
