using IT_Project_manager.Models;
using IT_Project_manager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace IT_Project_manager.Controllers
{
    public class ManagersController : Controller
    {
        private readonly IManagerService? _managerService;
        private readonly ILogger<ManagersController> _logger;

        public ManagersController(AppDbContext context, IManagerService? managerService)
        {
            _managerService = managerService;
        }

        //Managers list
        public async Task<IActionResult> Index()
        {
            return View( await _managerService.GetManagers() );
        }

        //Details [GET]
        [Authorize]
        [Authorize( Roles = "Administrator" )]
        public IActionResult Details(Manager manager)
        {
            if (manager is null)
            {
                return BadRequest( ModelState );
            }

            var found = _managerService.FindBy( manager.Id );
            return found is null ? NotFound() : View( found );
        }

        //Create [GET]
        [Authorize]
        [Authorize( Roles = "Administrator" )]
        public IActionResult Create()
        {
            Manager m = new Manager();
            return View(m);
        }
        //Create [POST]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manager manager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest( ModelState );
            }

            Manager newManager = await _managerService.CreateManager( manager );
            await _managerService.Save( newManager );

            return RedirectToAction("Index" );
        }

        //Editing [GET]
        [Authorize]
        [Authorize( Roles = "Administrator" )]
        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest( ModelState );
            }

            var manager = _managerService.FindBy( id );
            return manager is null ? NotFound() : View( manager );
        }

        //Editing [POST]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Manager manager)
        {

            if (ModelState.IsValid)
            {
                 await _managerService.Update( manager );

                var username = HttpContext.User.Identity.Name;
                

                return RedirectToAction( "Index" );

                
            }

            return View( manager );
        }
        //Deleting [GET]
        [Authorize]
        [Authorize( Roles = "Administrator" )]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest( ModelState );
            }

            if (await _managerService.Delete( id ))
            {
                return RedirectToAction( "Index" );
            }

            return Problem( "Trying to delete not existing member" );
        }

    }
}