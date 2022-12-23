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
        public IActionResult Index()
        {
            return View( _managerService.GetManagers() );
        }

        //Details [GET]
        [Authorize]
        [Authorize( Roles = "Administrator" )]
        public IActionResult Details(Manager manager)
        {
            if (manager is null)
            {
                return NotFound();
            }

            var found = _managerService.FindBy( manager.Id );
            return found is null ? NotFound() : View( found );
        }

        //Create [GET]
        [Authorize]
        [Authorize( Roles = "Administrator" )]
        public IActionResult Create()
        {
            return View();
        }
        //Create [POST]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Manager manager)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            Manager newManager = new Manager()
            {
                Name = manager.Name,
                Surname = manager.Surname,
                Telephone = manager.Telephone
            };

            _managerService.Save( newManager );

            return View( manager );
        }

        //Editing [GET]
        [Authorize]
        [Authorize( Roles = "Administrator" )]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var manager = _managerService.FindBy( id );
            return manager is null ? NotFound() : View( manager );
        }

        //Editing [POST]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Manager manager)
        {

            if (ModelState.IsValid)
            {
                _managerService.Update( manager );

                var username = HttpContext.User.Identity.Name;
                _logger.LogWarning( ( EventId )400, $"{manager.Id} edited by {username} on {DateTime.Now}" );

                return RedirectToAction( "Index" );

                
            }

            return View( manager );
        }
        //Deleting [GET]
        [Authorize]
        [Authorize( Roles = "Administrator" )]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (_managerService.Delete( id ))
            {
                return RedirectToAction( "Index" );
            }

            return Problem( "Trying to delete not existing member" );
        }

        //// POST: ManagersTest/Delete/5
        //[HttpPost, ActionName( "Delete" )]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_managerService.Managers == null)
        //    {
        //        return Problem( "Entity set 'AppDbContext.Managers'  is null." );
        //    }
        //    var manager = await _managerService.Managers.FindAsync( id );
        //    if (manager != null)
        //    {
        //        _managerService.Managers.Remove( manager );
        //    }

        //    await _managerService.SaveChangesAsync();
        //    return RedirectToAction( nameof( Index ) );
        //}
    }
}