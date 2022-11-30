using IT_Project_manager.Models;
using Microsoft.EntityFrameworkCore;

namespace IT_Project_manager.Controllers;

public class MembersServiceEF : IMemberService
{
    private readonly AppDbContext _context = new AppDbContext();

    public MembersServiceEF(AppDbContext context)
    {
        _context = context;
    }
    //Delete member
    public bool Delete(int? id)
    {
        if(id == null)
        {
            return false;
        }
        var member = _context.Members.Find( id );
        if( member is not null )
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
        if(id is null)
        {
            throw new ArgumentNullException("Member not found");
        }
        var member = _context.Members.Find( id ); 
        
        if( member is not null )
        {
            return member;
        }
        throw new ArgumentNullException( "Member not found" );

    }


    //Get members 
    public ICollection<Member> GetMembers()
    {
       return _context.Members.ToList();
    }

    //Add member
    public int Save(Member member)
    {
        var entityEntry = _context.Members.Add(member);
        _context.SaveChanges();
        return entityEntry.Entity.Id;
    }


    //Edit member
    public bool Update(Member member)
    {
        if(member == null )
        {
            return false;
        }
        try
        {
            var findMember = _context.Members.Find( member.Id );
            if( member is not null )
            {
                findMember.Name = member.Name;
                findMember.Surname = member.Surname;
                findMember.Email= member.Email;
                findMember.DateOfBirth= member.DateOfBirth;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        catch(DbUpdateConcurrencyException)
        {
            return false;
        }
    }
}
