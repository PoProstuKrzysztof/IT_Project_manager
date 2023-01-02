using IT_Project_manager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IT_Project_manager.Services;

public interface IMemberService
{
    public Task<int> Save(Member member);

    public Task<Member?> CreateMember(MembersViewModel member);

    public Task<bool> Delete(int? id);

    public Task<bool> Update(Member member);

    public Task<bool> AddManagerToMember(MembersViewModel memberModel, Member member);

    public Task<Member?> FindBy(int? id);

    public Task<ICollection<Member>> GetMembers();

    public Task<List<SelectListItem>> GetManagers();

    public Task<Manager> GetManager(int id);
}