using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace IT_Project_manager.Models.Identity;

public class RoleModification
{

    public string Id { get; set; }
    
    public string Name { get; set; }

    public string[]? AddIds { get; set; }
    public string[]? DeleteIds { get; set; }

}
