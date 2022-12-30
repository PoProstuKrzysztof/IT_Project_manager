using IT_Project_manager.Models;

namespace IT_Project_manager.Services;

public interface ITeamService
{
    public int Save(Team team);

    public bool Delete(Team team);

    public bool Update(Team team);

    public TeamsViewModel AddManagerToTeam(Team team);

    public TeamsViewModel AddMemberToTeam(Team team);

    public Team FindBy(int id);

}
