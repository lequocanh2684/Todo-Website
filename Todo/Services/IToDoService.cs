using System;
using Todo.DTO;

namespace Todo.Services
{
	public interface IToDoService
	{
        Task<List<ToDoDTO>?> GetListToDo(Guid userId);

        Task AddNewToDo(ToDoDTO toDoDTO, Guid userId);

        Task EditExistToDo(ToDoDTO newToDo, Guid toDoId);

        Task DeleteToDo(Guid toDoId);
    }
}

