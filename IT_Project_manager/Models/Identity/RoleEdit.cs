using IT_Project_manager.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace IT_Project_manager.Models.Identity;

public class RoleEdit
{
    public IdentityRole Role { get; set; }
    public IEnumerable<ApplicationUser> Members { get; set; }
    public IEnumerable<ApplicationUser> NonMembers { get; set; }    
}
