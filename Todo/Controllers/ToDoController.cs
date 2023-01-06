using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel.Resolution;
using Todo.DTO;
using Todo.Services;
using Todo.Forms;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Todo.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IToDoService _toDoService;
        private readonly UserManager<IdentityUser> _userManager;

        public ToDoController(IToDoService toDoService, UserManager<IdentityUser> identityUser) 
        { 
            _toDoService = toDoService;
            _userManager = identityUser;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View (await _toDoService.GetListToDo(_userManager.GetUserId(User)));
            
        }

        public async Task<IActionResult> Create([Bind("Title, IsCompleted")] ToDoForm toDoForm)
        {
            if (ModelState.IsValid)
            {
                await _toDoService.AddNewToDo(toDoForm, _userManager.GetUserId(User));
                return RedirectToAction(nameof(Index));
            }
            return View(toDoForm);
        }

        public async Task<IActionResult> Edit([Bind("Title, IsCompleted")] ToDoForm toDoForm, Guid toDoId)
        {
            await _toDoService.EditExistToDo(toDoForm, toDoId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid toDoId)
        {
            await _toDoService.DeleteToDo(toDoId);
            return RedirectToAction(nameof(Index));
        }
    }
}

