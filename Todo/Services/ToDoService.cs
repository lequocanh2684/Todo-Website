using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;
using Todo.DTO;
namespace Todo.Services
{
    public class ToDoService
	{
		public ToDoService()
		{
		}

		public async Task<List<ToDoDTO>?> GetListToDo(Guid userId)
		{
			try
			{
				using var context = new ToDoContext();
				var toDoList = await context.ToDoList.Where(todo => todo.UserId == userId).ToListAsync();
				return MapToDTO(toDoList);
			}catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
        }

		public async Task AddNewToDo(ToDoDTO toDoDTO, Guid userId)
		{
			try
			{
				using (var context = new ToDoContext())
				{
					var toDo = new ToDo()
					{
						Title = toDoDTO.Title,
						IsCompleted = toDoDTO.IsCompleted,
						CreatedAt = DateTime.Now,
						DeletedAt = null,
						CompletedAt = null,
						IsDeleted = false,
						UserId = userId
					};
					context.ToDoList.Add(toDo);
					await context.SaveChangesAsync();
				}
			}catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public async Task EditExistToDo(ToDoDTO newToDo, Guid toDoId)
		{
			try
			{
				using (var context = new ToDoContext())
				{
					var oldToDo = context.ToDoList.Find(toDoId);
					if (oldToDo != null)
					{
						oldToDo.Title = newToDo.Title;
						oldToDo.IsCompleted = newToDo.IsCompleted;
						oldToDo.CompletedAt = DateTime.Now;
						context.ToDoList.Update(oldToDo);
					}
					await context.SaveChangesAsync();
				}
			}catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public async Task DeleteToDo(Guid toDoId)
		{
			try
			{
				using var context = new ToDoContext();
				var toDoDelete = context.ToDoList.Where(toDo => toDo.Id == toDoId).FirstOrDefault();
				if (toDoDelete != null)
				{
					toDoDelete.IsDeleted = true;
					toDoDelete.DeletedAt = DateTime.Now;
					context.ToDoList.Update(toDoDelete);
				}
				await context.SaveChangesAsync();
			}catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private List<ToDoDTO> MapToDTO(List<ToDo> toDoList)
		{
			var toDoDTOList = new List<ToDoDTO>();
			foreach(var toDo in toDoList)
			{
				toDoDTOList.Add(new ToDoDTO(toDo));
			}
			return toDoDTOList;
		}
	}
}

