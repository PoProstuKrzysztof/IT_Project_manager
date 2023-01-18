using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IT_Project_manager.Areas.Identity.Repository;

public class RoleRepository : IRoleRepository
{
    private readonly IdentityDbContext _context;
    public RoleRepository(IdentityDbContext context)
    {
        _context = context;
    }

    public ICollection<IdentityRole> GetRoles()
    {
        return _context.Roles.ToList();
    }
}
