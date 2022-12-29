using System;
namespace Todo.Models
{
	public class ToDo
	{
		string? Id { get; set; }

		string? Titile { get; set; }

		bool IsCompleted { get; set; }

		public ToDo()
		{
		}
	}
}

