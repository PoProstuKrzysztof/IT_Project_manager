using IT_Project_manager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IT_Project_manager.Services;

public interface ITeamService
{
    public Task<int> Save(Team team);

    public Task<bool> Delete(int? id);

    public Task<bool> Update(Team team);

    public Task<bool> AddManagerToTeam(TeamsViewModel tvm,Team team);

    public Task<bool> AddMemberToTeam(TeamsViewModel tvm,Team team);

    public Task<Team> FindBy(int? id);

    public Task<IEnumerable<Team>> GetTeams();

    public List<SelectListItem> GetManagers();
    public List<SelectListItem> GetMembers();
    public TeamsViewModel GetMembersAndManagers(TeamsViewModel tvm);

    public Team CreateTeam(TeamsViewModel tvm);



}
