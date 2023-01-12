using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Todo.Models;
namespace Todo.Data
{
	public class ToDoContext : IdentityDbContext
	{
        public ToDoContext()
        {
        }

        public ToDoContext(DbContextOptions options) : base(options)
        {
		}

		public DbSet<ToDo>? ToDoList { get; set; }
    }
}

