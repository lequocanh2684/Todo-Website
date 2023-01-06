using System;
using Microsoft.EntityFrameworkCore;
using Todo.Models;
namespace Todo.Data
{
	public class ToDoContext : DbContext
	{
        public ToDoContext()
        {
        }

        public ToDoContext(DbContextOptions options) : base(options)
        {
		}

		public DbSet<ToDo>? ToDoList { get; set; }

        public DbSet<User>? User { get; set; }
    }
}

