using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Project_manager.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [Column( TypeName = "nvarchar(100)" )]
    public string? Name { get; set; }

    [PersonalData]
    [Column( TypeName = "nvarchar(100)" )]
    public string? Surname { get; set; }
}