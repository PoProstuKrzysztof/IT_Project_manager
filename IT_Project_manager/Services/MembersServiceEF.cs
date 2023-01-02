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
    public async Task<bool> Delete(int? id)
    {
        if (id == null)
        {
            return false;
        }
        var member = await _context.Members.FindAsync( id );
        if (member is not null)
        {
            _context.Members.Remove( member );
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    //Find member
    public async Task<Member?> FindBy(int? id)
    {
        if (id == null)
        {
            throw new ArgumentNullException( "Member not found" );
        }
        var memberFind = await _context.Members.FindAsync( id );

        if (memberFind != null)
        {
            return memberFind;
        }
        throw new ArgumentNullException( "Member not found" );
    }

    //Get members
    public async Task<ICollection<Member>> GetMembers()
    {
        return await _context.Members.Include( m => m.Managers ).ToListAsync();
    }

    //Save manager in database
    public async Task<int> Save(Member member)
    {
        var entityEntry = await _context.Members.AddAsync( member );
        await _context.SaveChangesAsync();
        return entityEntry.Entity.Id;
    }

    //Edit member

    public async Task<bool> Update(Member member)
    {
        if (member == null)
        {
            return false;
        }
        try
        {
            var findMember = await _context.Members.FindAsync( member.Id );
            if (member == null)
            {
                return false;
            }



            findMember.Name = member.Name;
            findMember.Surname = member.Surname;
            findMember.Email = member.Email;
            findMember.DateOfBirth = member.DateOfBirth;
            findMember.Managers = member.Managers;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }

    //Get managers
    public async Task<List<SelectListItem>> GetManagers()
    {
        return await _context
            .Managers
            .Select( m => new SelectListItem()
            {
                Value = m.Id.ToString(),
                Text = $"{m.Name} {m.Surname}"
            } )
            .ToListAsync();
    }

    //Find particular manager
    public async Task<Manager> GetManager(int id)
    {
        var manager = await _context.Managers.FindAsync( id );
        return manager;
    }

    //Add manager to member
    public async Task<bool> AddManagerToMember(MembersViewModel memberModel, Member member)
    {
        if (member == null)
        {
            return false;
        }

        foreach (var mId in memberModel.ManagersId)
        {
            if (int.TryParse( mId, out int id ))
            {
                var manager = await _context.Managers.FindAsync( id );
                member.Managers.Add( manager );
                return true;
            }
        }
        return false;
    }

    //Create member
    public async Task<Member?> CreateMember(MembersViewModel member)
    {
        Member newMember = new Member()
        {
            Name = member.Name,
            Surname = member.Surname,
            Email = member.Email,
            DateOfBirth = member.DateOfBirth
        };
        return newMember;
    }
}