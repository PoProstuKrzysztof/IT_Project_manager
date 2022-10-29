using Microsoft.AspNetCore.Mvc;

namespace IT_Project_manager.Controllers
{
    public class MemberController : Controller
    {

        [HttpGet]
        public IActionResult MemberForm()
        {
            return View();
        }
    }
}
