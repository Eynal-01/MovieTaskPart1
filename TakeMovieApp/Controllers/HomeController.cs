using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TakeMovieApp.Models;

namespace TakeMovieApp.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

      
    }
}
