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
using Microsoft.CodeAnalysis.CSharp;

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
		[Fact]
		public async void AddToDo_ReturnSuccess()
		{
			var tempListToDoTest = new List<ToDoDTO>
			{
				new ToDoDTO()
				{
					Id = Guid.Parse("7a7cbb2f-1173-44b5-8f78-84c4f4202947"),
					Title = "test3",
					IsCompleted = false
				},
				new ToDoDTO()
				{
					Id = Guid.Parse("7a7cbb2f-1173-44b5-8f78-84c4f4202944"),
					Title = "test4",
					IsCompleted = false
				}
			};
            var _toDoServiceTest = new ToDoService(_context.Object, _mapper);
			var _newToDo = new ToDoForm()
			{
				Title= "test4",
				IsCompleted = false,
			};
			await _toDoServiceTest.AddNewToDo(_newToDo, "37f96471-a1c2-4a59-8b28-41671e3c2749");
            var result = await _toDoServiceTest.GetListToDo("37f96471-a1c2-4a59-8b28-41671e3c2749");
			CollectionAssert.Equals(tempListToDoTest, result);
        }

		[Fact]
		public async void EditToDo_ReturnSuccess()
		{
			var tempToDoTest = new ToDoDTO()
			{
				Id = Guid.Parse("7a7cbb2f-1173-44b5-8f78-84c4f4202947"),
				Title = "test_edited",
				IsCompleted = false
			};
            var _toDoServiceTest = new ToDoService(_context.Object, _mapper);
			var check = _toDoServiceTest.EditExistToDo(tempToDoTest, Guid.Parse("7a7cbb2f-1173-44b5-8f78-84c4f4202947")).IsCompletedSuccessfully;
			check.Should().BeTrue();
			var result = await _toDoServiceTest.GetListToDo("37f96471-a1c2-4a59-8b28-41671e3c2749");
			CollectionAssert.Equals(_toDoServiceTest, result);
        }

		[Fact]
		public async void DeleteToDo_ReturnSuccess()
		{
			/*var tempToDoTest = new ToDoDTO()
			{
                Id = Guid.Parse("7a7cbb2f-1173-44b5-8f78-84c4f4263677"),
                Title = "test_edited",
                IsCompleted = false
            };*/
			var _toDoServiceTest = new ToDoService(_context.Object, _mapper);
			var check = _toDoServiceTest.DeleteToDo(Guid.Parse("7a7cbb2f-1173-44b5-8f78-84c4f4228471")).IsCompletedSuccessfully;
			check.Should().BeTrue();
			var check1 = _context.Object.ToDoList.Find(Guid.Parse("7a7cbb2f-1173-44b5-8f78-84c4f4228471"));
			check1.IsDeleted.Should().BeTrue();
/*			var result = await _toDoServiceTest.GetListToDo("37f96471-a1c2-4a59-8b28-41671e3c1609");
*//*			CollectionAssert.Equals(_toDoServiceTest, result);
*/		}
    }
}

