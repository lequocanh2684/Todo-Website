using System;
using System.ComponentModel.DataAnnotations;
namespace Todo.Models
{
	public class ToDo
	{
		[Key]
		public Guid Id { get; set; }

		public string? Titile { get; set; }

		public bool IsCompleted { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime CompletedAt { get; set; }

		public ToDo()
		{
		}
	}
}

