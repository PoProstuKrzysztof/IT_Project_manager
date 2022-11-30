using Microsoft.EntityFrameworkCore;

namespace IT_Project_manager.Models;

public interface IMemberService
{
    public int Save(Member member);
    public bool Delete(int? id);
    public bool Update(Member member);
    public Member? FindBy(int? id);

    public ICollection<Member> GetMembers();

}
