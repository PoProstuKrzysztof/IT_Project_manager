using IT_Project_manager.Data;
using IT_Project_manager.Models;
using IT_Project_manager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace IT_Project_manager.Controllers
{

    public class ManagersController : Controller
    {
        private readonly IManagerService? _managerService;
        

        public ManagersController(AppDbContext context, IManagerService? managerService)
        {
            _managerService = managerService;
        }

        //Managers list
        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            try
            {
                ViewData["CurrentFilter"] = search;
                var managers = from c in await _managerService.GetManagers() 
                               select c;
                if(!search.IsNullOrEmpty())
                {
                    managers = managers.Where(managers => managers.Name == search);
                }

                return View( managers );
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex );
                return StatusCode( 500, ex.Message );
            }
        }

        //Details [GET]
        
        [HttpGet, Authorize( Roles = "Administrator" )]        
        public IActionResult Details(Manager manager)
        {
            try
            {
                if (manager is null)
                {
                    return BadRequest( ModelState );
                }

                var found = _managerService.FindBy( manager.Id );
                return found is null ? NotFound() : View( found );
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex );
                return StatusCode( 500, ex.Message );
            }
        }

        //Create [GET]

        [HttpGet, Authorize( Roles = "Administrator" )]
        public IActionResult Create()
        {
            try
            {
                Manager m = new Manager();
                return View( m );
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex );
                return StatusCode( 500, ex.Message );
            }
        }

        //Create [POST]
        [HttpPost, Authorize( Roles = "Administrator" )]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manager manager)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest( ModelState );
                }

                Manager newManager =  _managerService.CreateManager( manager );
                await _managerService.Save( newManager );

                return RedirectToAction( "Index" );
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex );
                return StatusCode( 500, ex.Message );
            }
        }

        //Editing [GET]        
        [HttpGet, Authorize( Roles = "Administrator" )]       
        public IActionResult Edit(int? id)
        {
            try
            {
                if (id is null)
                {
                    return BadRequest( ModelState );
                }

                var manager = _managerService.FindBy( id );
                return manager is null ? NotFound() : View( manager );
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex );
                return StatusCode( 500, ex.Message );
            }
        }

        //Editing [POST]
        [HttpPost, Authorize( Roles = "Administrator" )]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Manager manager)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _managerService.Update( manager );

                    var username = HttpContext.User.Identity.Name;

                    return RedirectToAction( "Index" );
                }

                return View( manager );
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex );
                return StatusCode( 500, ex.Message );
            }
        }

        //Deleting [GET]
        [HttpGet, Authorize( Roles = "Administrator" )]
        
        
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest( ModelState );
                }

                if (await _managerService.Delete( id ))
                {
                    return RedirectToAction( "Index" );
                }

                return StatusCode( 500, "Trying to delete not existing member" );
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex );
                return StatusCode( 500, ex.Message );
            }
        }
    }
}