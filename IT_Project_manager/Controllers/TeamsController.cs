using IT_Project_manager.Models;
using IT_Project_manager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IT_Project_manager.Controllers;

public class TeamsController : Controller
{
    private readonly ITeamService? _teamService;

    public TeamsController(AppDbContext context, ITeamService service)
    {
        _teamService = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var teams = await _teamService.GetTeams();
            return View( teams );
        }
        catch (Exception ex)
        {
            Console.WriteLine( ex );
            return StatusCode( 500, ex.Message );
        }
    }

    [HttpGet, Authorize( Roles = "Administrator" )]
    public IActionResult Create()
    {
        try
        {
            TeamsViewModel newTeam = new TeamsViewModel();
            return View( _teamService.GetMembersAndManagers( newTeam ) );
        }
        catch (Exception e)
        {
            Console.WriteLine( e );
            return StatusCode( 500, e.Message );
        }
    }

    [HttpPost, Authorize( Roles = "Administrator" )]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TeamsViewModel tvm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View( _teamService.GetMembersAndManagers( tvm ) );
            }

            var newTeam = _teamService.CreateTeam( tvm );

            if (await _teamService.AddManagerToTeam( tvm, newTeam ) && await _teamService.AddMemberToTeam( tvm, newTeam ))
            {
                await _teamService.Save( newTeam );
                return RedirectToAction( nameof( Index ) );
            }
            else
            {
                return View( _teamService.GetMembersAndManagers( tvm ) );
            }
        }
        catch (Exception e)
        {
            Console.WriteLine( e );
            return View( "Index" );
        }
    }

    [Authorize( Roles = "Administrator" )]
    public async Task<IActionResult> Delete(int? id)
    {
        try
        {
            if (id == null)
            {
                return RedirectToAction( "Index" );
            }
            if (await _teamService.Delete( id ))
            {
                return RedirectToAction( "Index" );
            }
            return BadRequest( "Trying to delete not existing team" );
        }
        catch (Exception e)
        {
            Console.WriteLine( e );
            return StatusCode( 500, e.Message );
        }
    }
}