using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;
using Todo.Forms;
using Todo.DTO;
using AutoMapper;

namespace Todo.Services
{
    public class ToDoService : IToDoService
	{
		private readonly ToDoContext _context;
		private readonly IMapper _mapper;
		public ToDoService(ToDoContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<List<ToDoDTO>?> GetListToDo(string userId)
		{
			try
			{
				var toDoList = await _context.ToDoList.Where(todo => todo.UserId == userId && !todo.IsDeleted).ToListAsync();

				//var toDoList = (from todo in _context.ToDoList where todo.UserId == userId where !todo.IsDeleted select todo).ToList();
				return _mapper.Map<List<ToDoDTO>>(toDoList);
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

		public async Task EditExistToDo(ToDoDTO toDo, Guid toDoId)
		{
			try
			{
				var oldToDo = _context.ToDoList.Find(toDoId);
				if (oldToDo != null)
				{
					oldToDo.Title = toDo.Title;
					oldToDo.IsCompleted = toDo.IsCompleted;
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

		public async Task<ToDoDTO?> FindToDo(Guid toDoId)
		{
			try
			{
				var toDo = _mapper.Map<ToDoDTO>(await _context.ToDoList.FindAsync(toDoId));
				return toDo;
			}catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}
	}
}

