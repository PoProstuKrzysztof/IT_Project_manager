using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IT_Project_manager.Models;

public class MembersViewModel
{
    public MembersViewModel()
    {
        ManagersId = new List<string>();
    }
    [ValidateNever]
    public List<SelectListItem> Managers { get; set; }

    [Required]
    [Display( Name = "Name" )]
    public string Name { get; set; }

    [Required]
    [Display( Name = "Surname" )]
    public string Surname { get; set; }

    [Required]
    [Display(Name ="Email")]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Display(Name="Date of birth")]
    [DataType( DataType.Date )]
    public DateTime DateOfBirth { get; set; }

    [Display( Name = "Managers" )]
    public List<string> ManagersId { get; set; }
}
