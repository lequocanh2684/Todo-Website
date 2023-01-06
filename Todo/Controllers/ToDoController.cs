using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel.Resolution;
using Todo.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Todo.Controllers
{
    public class ToDoController : Controller
    {
        /*private readonly ToDoService _toDoService;

        public ToDoController(ToDoService toDoService) 
        { 
            _toDoService = toDoService;
        } */

        // GET: /<controller>/
        public IActionResult Index()
        {
/*            _toDoService.AddNewToDo();
*/            return View();
        }
        
    }
}

