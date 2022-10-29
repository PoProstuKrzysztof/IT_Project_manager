using IT_Project_manager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IT_Project_manager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult ContactForm(Contact contact)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return View("ConiformContact", contact);
        //    }               
        //    else
        //    {
        //        return View();
        //    }               

        //}

        [HttpGet]
        public IActionResult ContactForm(Contact contact)
        {
            if(ModelState.IsValid)
            {
                return View("ConiformContact", contact);
            }
            else
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}