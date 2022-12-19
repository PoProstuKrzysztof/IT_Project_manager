using IT_Project_manager.Models;
using IT_Project_manager.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IT_Project_manager.Controllers;

public class MembersServiceEF : IMemberService
{
    private readonly AppDbContext _context;

    public MembersServiceEF(AppDbContext context)
    {
        _context = context;
    }

    //Delete member
    public bool Delete(int? id)
    {
        if (id == null)
        {
            return false;
        }
        var member = _context.Members.Find( id );
        if (member is not null)
        {
            _context.Members.Remove( member );
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    //Find member
    public Member? FindBy(int? id)
    {
        if (id is null)
        {
            throw new ArgumentNullException( "Member not found" );
        }
        var member = _context.Members.Find( id );

        if (member is not null)
        {
            return member;
        }
        throw new ArgumentNullException( "Member not found" );
    }

    //Get members
    public ICollection<Member> GetMembers()
    {
        return _context.Members.Include( m => m.Managers ).ToList();
    }

    //Save manager in database
    public int Save(Member member)
    {
        var entityEntry = _context.Members.Add( member );
        _context.SaveChanges();
        return entityEntry.Entity.Id;
    }

    //Edit member
    public bool Update(Member member)
    {
        if (member == null)
        {
            return false;
        }
        try
        {
            var findMember = _context.Members.Find( member.Id );
            if (member is not null)
            {
                findMember.Name = member.Name;
                findMember.Surname = member.Surname;
                findMember.Email = member.Email;
                findMember.DateOfBirth = member.DateOfBirth;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }

    //Get managers
    public List<SelectListItem> GetManagers()
    {
        return _context
            .Managers
            .Select( m => new SelectListItem()
            {
                Value = m.Id.ToString(),
                Text = $"{m.Name} {m.Surname} {m.Telephone}"
            } )
            .ToList();
    }

    //Find particular manager
    public Manager GetManager(int id)
    {
        var manager = _context.Managers.Find( id );
        return manager;
    }

    public bool AddManagerToMember(MembersViewModel memberModel, Member member)
    {
        if (member == null)
        {
            return false;
        }

        foreach (var mId in memberModel.ManagersId)
        {
            if (int.TryParse( mId, out int id ))
            {
                var manager = _context.Managers.Find( id );
                member.Managers.Add( manager );
                return true;
            }
        }
        return false;
    }
}