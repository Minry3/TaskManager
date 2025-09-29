using System;

public class TaskException : Exception
{
	private string message;
	public string Message { get => message; }
	public TaskException() { }
	
	public TaskException(string message)
	{
		this.message = message;
	}

	public string ToString()
	{
		return this.message; 
	}
}
