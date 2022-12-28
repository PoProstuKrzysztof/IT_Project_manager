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

    [StringLength( 50 )]
    public string? Name { get; set; }

    [StringLength( 250 )]
    [DataType( DataType.MultilineText )]
    public string? Description { get; set; }

    [Required]
    [DataType( DataType.Date )]
    public DateTime AssigmentDate { get; set; } = DateTime.UtcNow;

    [Required]
    [DataType( DataType.Date )]
    public DateTime DeadlineDate { get; set; }


    [Display(Name = "Members")]
    public List<MultiSelectList> Members { get; set; }
    public List<string> MembersId { get; set; }

    public List<SelectList> Managers { get; set; }
    [Display(Name = "Managers")]
    public List<string> ManagersId { get; set; }


}
