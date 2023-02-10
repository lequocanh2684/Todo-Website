using System;
using Todo.Models;
using Todo.Data;
using Microsoft.EntityFrameworkCore;
namespace TodoXUnitTest.Repositories
{
	public class ToDoRepositoryFake
	{
		public ToDoRepositoryFake()
		{
		}

		internal async Task<ToDoContext> GetToDoContext()
		{
			var options = new DbContextOptionsBuilder<ToDoContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;
			var databaseContext = new ToDoContext(options);
			databaseContext.Database.EnsureCreated();
			databaseContext.ToDoList.Add(
				new ToDo
				{
					Id = Guid.Parse("7a7cbb2f-1173-44b5-8f78-84c4f4263677"),
					Title = "test",
					IsCompleted = false,
					IsDeleted = false,
					CreatedAt = DateTime.Now,
					CompletedAt = null,
					DeletedAt = null,
					UserId = "37f96471-a1c2-4a59-8b28-41671e3c1609"
				});
            databaseContext.ToDoList.Add(
                new ToDo
                {
                    Id = Guid.Parse("7a7cbb2f-1173-44b5-8f78-84c4f4228471"),
                    Title = "test2",
                    IsCompleted = false,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    CompletedAt = null,
                    DeletedAt = null,
                    UserId = "37f96471-a1c2-4a59-8b28-41671e3c1609"
                });
            databaseContext.ToDoList.Add(
                new ToDo
                {
                    Id = Guid.Parse("7a7cbb2f-1173-44b5-8f78-84c4f4202947"),
                    Title = "test3",
                    IsCompleted = false,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    CompletedAt = null,
                    DeletedAt = null,
                    UserId = "37f96471-a1c2-4a59-8b28-41671e3c2749"
                });
            await databaseContext.SaveChangesAsync();
			return databaseContext;
		}
	}
}

