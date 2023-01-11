using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;
using Todo.Forms;
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

		public async Task<List<ToDoDTO>?> GetListToDo(string userId)
		{
			try
			{
				var toDoList = await _context.ToDoList.Where(todo => todo.UserId == userId && !todo.IsDeleted).ToListAsync();
				return MapToDTOList(toDoList);
			}catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
        }

		public async Task AddNewToDo(ToDoForm toDoForm, string userId)
		{
			try
			{ 
					var toDo = new ToDo()
					{
						Title = toDoForm.Title,
						IsCompleted = toDoForm.IsCompleted,
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

		public async Task EditExistToDo(ToDoDTO toDoForm, Guid toDoId)
		{
			try
			{
				var oldToDo = _context.ToDoList.Find(toDoId);
				if (oldToDo != null)
				{
					oldToDo.Title = toDoForm.Title;
					oldToDo.IsCompleted = toDoForm.IsCompleted;
					if (oldToDo.IsCompleted)
					{
						oldToDo.CompletedAt = DateTime.Now;
					}
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

		public async Task<ToDoDTO>? FindToDo(Guid toDoId)
		{
			try
			{
				var toDo = await _context.ToDoList.FindAsync(toDoId);
				return new ToDoDTO(toDo);
			}catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}

		private List<ToDoDTO> MapToDTOList(List<ToDo> toDoList)
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

