using System;
using Todo.Forms;
using Todo.DTO;

namespace Todo.Services
{
	public interface IToDoService
	{
        Task<List<ToDoDTO>?> GetListToDo(string userId);

        Task AddNewToDo(ToDoForm toDoForm, string userId);

        Task EditExistToDo(ToDoDTO toDoForm, Guid toDoId);

        Task DeleteToDo(Guid toDoId);

        Task<ToDoDTO> FindToDo(Guid toDoId);
    }
}

