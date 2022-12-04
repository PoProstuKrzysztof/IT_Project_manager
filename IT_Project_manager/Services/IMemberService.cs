using IT_Project_manager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IT_Project_manager.Services;

public interface IMemberService
{
    public int Save(Member member);
    public bool Delete(int? id);
    public bool Update(Member member);
    public Member? FindBy(int? id);

    public ICollection<Member> GetMembers();
    public List<SelectListItem> GetManagers();
    Manager GetManager(int id);
}
