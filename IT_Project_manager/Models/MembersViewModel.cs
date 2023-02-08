using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace IT_Project_manager.Models;

public class MembersViewModel
{
    public MembersViewModel()
    {
        ManagersId = new List<string>();
    }

    [ValidateNever]
    [JsonIgnore]
    public List<SelectListItem> Managers { get; set; }

    [Display( Name = "Managers" )]
    public List<string> ManagersId { get; set; }

    [Required( ErrorMessage = "Name is required" )]
    [Display( Name = "Name" )]
    public string Name { get; set; }

    [Required( ErrorMessage = "Surname is required" )]
    [Display( Name = "Surname" )]
    public string Surname { get; set; }

    [EmailAddress( ErrorMessage = "Enter your email" )]
    [Display( Name = "Email" )]
    [DataType( DataType.EmailAddress )]
    public string? Email { get; set; }

    [Display( Name = "Date of birth" )]
    [DataType( DataType.Date )]
    public DateTime DateOfBirth { get; set; }
}