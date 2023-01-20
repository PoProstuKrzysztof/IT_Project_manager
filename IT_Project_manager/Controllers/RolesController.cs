using IT_Project_manager.Areas.Identity.Data;
using IT_Project_manager.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace IT_Project_manager.Controllers;
public class RolesController : Controller
{
    private RoleManager<IdentityRole> _roleManager;
    private UserManager<ApplicationUser> _userManager;

    public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }
    //List
    public IActionResult Index()
    {
        return View( _roleManager.Roles );
    }

    private void Errors(IdentityResult result)
    {
        foreach (IdentityError error in result.Errors)
        {
            ModelState.AddModelError( "", error.Description );
        }
    }

    //Create[GET]
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    //Create[POST]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string name)
    {
        try
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync( new IdentityRole( name ) );
                if (result.Succeeded)
                {
                    return RedirectToAction( "Index" );
                }
                else
                {
                    Errors( result );
                }

            }
            return View( name );


        }
        catch (Exception ex)
        {
            Console.WriteLine( ex );
            return StatusCode( 500, ex.Message );
        }
    }

    //Delete[GET]
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            IdentityRole role = await _roleManager.FindByIdAsync( id );
            if (role == null)
            {
                ModelState.AddModelError( "", "Role not found" );
                return View( "Index", _roleManager.Roles );
            }


            IdentityResult result = await _roleManager.DeleteAsync( role );
            if (result.Succeeded)
            {
                return RedirectToAction( "Index" );
            }
            else
            {
                Errors( result );
            }

            return View( "Index", _roleManager.Roles );
        }
        catch (Exception ex)
        {
            Console.WriteLine( ex );
            return StatusCode( 500, ex.Message );

        }
    }


    //Update[GET]
    public async Task<IActionResult> Update(string id)
    {
        try
        {
            IdentityRole role = await _roleManager.FindByIdAsync( id );
            List<ApplicationUser> members = new List<ApplicationUser>();
            List<ApplicationUser> nonMembers = new List<ApplicationUser>();

            foreach (ApplicationUser user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync( user, role.Name ) ? members : nonMembers;
                list.Add( user );

            }
            return View(new RoleEdit
            {
                Role= role,
                Members = members,
                NonMembers = nonMembers
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine( ex );
            return StatusCode( 500, ex.Message );
        }
    }
    //Update[POST]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(RoleModification model)
    {
        try
        {
            IdentityResult result;
            if(ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    ApplicationUser user = await _userManager.FindByIdAsync( userId );
                    if(user != null)
                    {
                        result = await _userManager.AddToRoleAsync( user, model.Name );
                        if(!result.Succeeded)
                        {
                            Errors( result );
                        }
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] {})
                {
                    ApplicationUser user = ( ApplicationUser )await _userManager.FindByIdAsync( userId );
                    if(user != null )
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, model.Name);
                    }
                }
            }
           

            return await Update( model.Id );
        }
        catch (Exception ex)
        {
            Console.WriteLine( ex );
            return StatusCode(500, ex.Message );
        }
    }
}
