using Microsoft.AspNetCore.Identity;

namespace IT_Project_manager.Areas.Identity.Repository;


public interface IRoleRepository
{
    ICollection<IdentityRole> GetRoles();
}
