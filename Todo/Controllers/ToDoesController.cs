using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.DTO;
using Todo.Forms;
using Todo.Models;
using Todo.Services;

namespace Todo.Controllers
{
    public class ToDoesController : Controller
    {
        private readonly IToDoService _toDoService;
        private readonly UserManager<IdentityUser> _identityUser;

        public ToDoesController(IToDoService toDoService,UserManager<IdentityUser> identityUser)
        {
            _toDoService = toDoService;
            _identityUser = identityUser;
        }

        // GET: ToDoes
        public async Task<IActionResult> Index()
        {
              return View(await _toDoService.GetListToDo(_identityUser.GetUserId(User)));
        }

        // GET: ToDoes/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (_toDoService.GetListToDo(_identityUser.GetUserId(User)) == null)
            {
                return NotFound();
            }

            var toDo = await _toDoService.FindToDo(id);
            if (toDo == null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        // GET: ToDoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoForm toDoForm)
        {
            if (ModelState.IsValid)
            {
                string id = _identityUser.GetUserId(User);
                await _toDoService.AddNewToDo(toDoForm, id);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: ToDoes/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var toDo = _toDoService.FindToDo(id).Result;
            if (toDo == null)
            {
                return NotFound();
            }
            return View(toDo);
        }

        // POST: ToDoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> EditToDo(ToDoDTO toDoDTO, Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _toDoService.EditExistToDo(toDoDTO, id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_toDoService.FindToDo(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ToDoes/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var toDo = _toDoService.FindToDo(id);
            if (toDo == null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        // POST: ToDoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_toDoService.GetListToDo(_identityUser.GetUserId(User)) == null)
            {
                return Problem("Entity set 'ToDoContext.ToDoList'  is null.");
            }
            var toDo = await _toDoService.FindToDo(id);
            if (toDo != null)
            {
                await _toDoService.DeleteToDo(id);
            }
            return RedirectToAction(nameof(Index));
        }

        public class Bind
        {
        }

        /*private bool ToDoExists(Guid id)
        {
            if(_toDoService.FindToDo(id) != null)
            return ToDoExists(id);
        }*/
    }
}
