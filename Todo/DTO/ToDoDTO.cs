using System;
using Todo.Models;
namespace Todo.DTO
{
	public class ToDoDTO
	{
		public string? Title { get; set; }

        public bool IsCompleted { get; set; }

		public ToDoDTO(ToDo toDo)
		{
			Title = toDo.Title;
			IsCompleted = toDo.IsCompleted;
		}

		public ToDoDTO() { }
    }
}

