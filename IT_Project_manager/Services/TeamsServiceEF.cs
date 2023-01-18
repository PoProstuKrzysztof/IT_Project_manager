using IT_Project_manager.Data;
using IT_Project_manager.Models;
using IT_Project_manager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IT_Project_manager.Services;

public class TeamsServiceEF : ITeamService
{
    private readonly AppDbContext _context;

    public TeamsServiceEF(AppDbContext context)
    {
        _context = context;
    }

    //List of Teams
    public async Task<IEnumerable<Team>> GetTeams()
    {
        var teamsManager = await _context.Teams.Include( m => m.Managers ).ToListAsync();
        var teamsMember = await _context.Teams.Include( m => m.Members ).ToListAsync();
        var teams = teamsManager.Union( teamsMember );
        return teams;
    }

    //Delete Teams
    public async Task<bool> Delete(int? id)
    {
        if (id == null)
        {
            return false;
        }

        var teamFind = await _context.Teams.FindAsync( id );
        if (teamFind == null)
        {
            return false;
        }


        _context.Teams.Remove( teamFind );
        await _context.SaveChangesAsync();
        return true;

    }

    //Find Team
    public async Task<Team> FindBy(int? id)
    {
        if (id == null)
        {
            throw new ArgumentNullException( "Team not found" );
        }

        var teamFind = await _context.Teams.FindAsync( id );

        if (teamFind != null)
        {
            return teamFind;
        }

        throw new ArgumentNullException( "Member not found" );
    }

    //Add Member to Team
    public async Task<bool> AddMemberToTeam(TeamsViewModel tvm, Team team)
    {
        foreach (var memberId in tvm.MembersId)
        {
            if (int.TryParse( memberId, out int id ))
            {
                var member = _context.Members.Find( id );
                if (team.Members.Add( member ))
                {
                    _context.Teams.Add( team );
                }

            }
        }
        await _context.SaveChangesAsync();
        return true;
    }

    //Add Manager to Team
    public async Task<bool> AddManagerToTeam(TeamsViewModel tvm, Team team)
    {
        if (team == null)
        {
            return false;
        }

        foreach (var managerId in tvm.ManagersId)
        {
            if (int.TryParse( managerId, out int id ))
            {
                var manager = await _context.Managers.FindAsync( id );
                if (team.Managers.Add( manager ))
                {
                    await _context.SaveChangesAsync();
                    continue;
                }
                else
                {
                    return false;
                }
            }

        }
        return true;
    }



    //Save Team
    public async Task<int> Save(Team team)
    {
        var entityEntry = await _context.Teams.AddAsync( team );
        await _context.SaveChangesAsync();
        return entityEntry.Entity.Id;
    }

    //Edit team
    public async Task<bool> Update(Team team)
    {
        if(team == null)
        {
            return false;
        }

        try
        {
            var findTeam = await _context.Teams.FindAsync( team.Id );
            if(findTeam == null)
            {
                return false ;
            }


            findTeam.Name = team.Name;
            findTeam.Description = team.Description;
            findTeam.DeadlineDate = team.DeadlineDate;
            return true;
        }
        catch(DbUpdateConcurrencyException)
        {
            return false;
        }

    }

    //Get Managers for list 
    public List<SelectListItem> GetManagers()
    {
        return _context.Managers
            .Select( m => new SelectListItem()
            {
                Value = m.Id.ToString(),
                Text = $"{m.Name} {m.Surname}"
            } )
            .ToList();
    }
    //Get Members for list
    public List<SelectListItem> GetMembers()
    {
        return _context.Members
    .Select( m => new SelectListItem()
    {
        Value = m.Id.ToString(),
        Text = $"{m.Name} {m.Surname}"
    } )
    .ToList();
    }

    //Get Members and Managers for list
    public TeamsViewModel GetMembersAndManagers(TeamsViewModel tvm)
    {
         tvm.Managers = GetManagers();
        tvm.Members = GetMembers();
        return tvm;
    }

    public Team CreateTeam(TeamsViewModel tvm)
    {
        Team newTeam = new Team()
        {
            Name = tvm.Name,
            Description = tvm.Description,
            AssigmentDate = tvm.AssigmentDate,
            DeadlineDate = tvm.DeadlineDate
        };
        return newTeam;
    }
}
