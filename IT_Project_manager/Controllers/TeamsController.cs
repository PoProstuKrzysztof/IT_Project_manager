using Microsoft.AspNetCore.Http;
using IT_Project_manager;
using Microsoft.AspNetCore.Mvc;
using IT_Project_manager.Models;
using Microsoft.EntityFrameworkCore;

namespace IT_Project_manager.Controllers;
[Route( "api/[controller]" )]
[ApiController]
public class TeamsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TeamsController(AppDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<ActionResult<List<Team>>> Get(int TeamId)
    {
        var teams = await _context.Teams
            //.Include(m => m.Manager)
            .ToListAsync();

        return teams;
    }


    [HttpPost]
    public async Task<ActionResult> Create(TeamViewModel tvm)
    {
        
        var newTeam = new Team()
        { 
            Name = tvm.Name,
            Description = tvm.Description,
            AssigmentDate= tvm.AssigmentDate,
            DeadlineDate= tvm.DeadlineDate
        };
        _context.Teams.Add(newTeam);
        await _context.SaveChangesAsync();

        return Ok(Get(newTeam.Id));
    }
}
