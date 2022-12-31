using System;
using Microsoft.EntityFrameworkCore;
using Todo.Models;
namespace Todo.Data
{
	public class ToDoContext : DbContext
	{
		
		public ToDoContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<ToDo> Todo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ToDo>(entity => entity.HasKey(e => e.Id));

        }
    }
}

