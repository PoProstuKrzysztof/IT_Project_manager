using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IT_Project_manager.Models;

public class TeamsViewModel
{
    public TeamsViewModel()
    {
        ManagersId = new List<string>();
        MembersId = new List<string>();
    }

    [Required( ErrorMessage = "You must set team name!" )]
    [StringLength( 50 )]
    public string? Name { get; set; }

    [Required( ErrorMessage = "You must set description of a task!" )]
    [StringLength( 250 )]
    [DataType( DataType.MultilineText )]
    public string? Description { get; set; }

    [Required]
    [DataType( DataType.Date )]
    public DateTime AssigmentDate { get; set; } = DateTime.UtcNow;

    [Required( ErrorMessage = "You must set deadline date!" )]
    [DataType( DataType.Date )]
    public DateTime DeadlineDate { get; set; }

    [ValidateNever]
    public List<SelectListItem> Members { get; set; }

    public List<string> MembersId { get; set; }

    [ValidateNever]
    public List<SelectListItem> Managers { get; set; }

    public List<string> ManagersId { get; set; }
}