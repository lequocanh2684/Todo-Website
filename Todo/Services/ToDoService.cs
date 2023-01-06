using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;
using Todo.DTO;
namespace Todo.Services
{
    public class ToDoService : IToDoService
	{
		private readonly ToDoContext _context;
		public ToDoService(ToDoContext context)
		{
			_context = context;
		}

		public async Task<List<ToDoDTO>?> GetListToDo(Guid userId)
		{
			try
			{
				var toDoList = await _context.ToDoList.Where(todo => todo.UserId == userId).ToListAsync();
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
					_context.ToDoList.Add(toDo);
					await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public async Task EditExistToDo(ToDoDTO newToDo, Guid toDoId)
		{
			try
			{
					var oldToDo = _context.ToDoList.Find(toDoId);
					if (oldToDo != null)
					{
						oldToDo.Title = newToDo.Title;
						oldToDo.IsCompleted = newToDo.IsCompleted;
						oldToDo.CompletedAt = DateTime.Now;
						_context.ToDoList.Update(oldToDo);
					}
					await _context.SaveChangesAsync();
			}catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public async Task DeleteToDo(Guid toDoId)
		{
			try
			{
				var toDoDelete = _context.ToDoList.Where(toDo => toDo.Id == toDoId).FirstOrDefault();
				if (toDoDelete != null)
				{
					toDoDelete.IsDeleted = true;
					toDoDelete.DeletedAt = DateTime.Now;
					_context.ToDoList.Update(toDoDelete);
				}
				await _context.SaveChangesAsync();
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

