using System;
using Moq;
using Todo.Services;
using Todo.Models;
using Todo.DTO;
using Todo.Forms;
using Todo.Data;
using TodoXUnitTest.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace TodoXUnitTest
{
	public class ToDoServiceTest
	{
		//private readonly IToDoService _toDoServiceTest;
		private readonly ToDoRepositoryFake _toDoRepositoryTest;
		private readonly IMapper _mapper;
		//private Mock<UserManager<IdentityUser>> _user;
		private Mock<ToDoContext> _context = new();

		public ToDoServiceTest()
		{
			_toDoRepositoryTest = new();
			_mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<ToDo, ToDoDTO>()));
			//_toDoServiceTest = new ToDoService(_toDoRepositoryTest.GetToDoContext().Result, _mapper);
			_context.Setup(s => s.ToDoList).Returns(_toDoRepositoryTest.GetToDoContext().Result.ToDoList);
		}

		[Fact]
		public async void GetListToDo_ReturnSuccess()
		{
			var tempListToDoTest = new List<ToDoDTO>()
			{
				new ToDoDTO()
				{
					Id = Guid.Parse("7a7cbb2f-1173-44b5-8f78-84c4f4263677"),
					Title = "test",
					IsCompleted = false
				},
				new ToDoDTO()
				{
                    Id = Guid.Parse("7a7cbb2f-1173-44b5-8f78-84c4f4228471"),
                    Title = "test2",
                    IsCompleted = false
                }
			};
			var _toDoServiceTest = new ToDoService(_context.Object, _mapper);
			var result = await _toDoServiceTest.GetListToDo("37f96471-a1c2-4a59-8b28-41671e3c1609");
			CollectionAssert.Equals(tempListToDoTest, result);
		}
	}
}

