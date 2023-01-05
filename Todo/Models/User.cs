using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
	public class User
	{
		[Key]
		public Guid? Id { get; set; }

		[Required, MinLength(1)]
		public string? UserName { get; set; }

		[Required, MinLength(1)]
		public string? Password { get; set; }
	}
}

