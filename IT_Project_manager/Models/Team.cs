using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xunit.Sdk;

namespace IT_Project_manager.Models;

[Table( "Teams" )]
public class Team
{
    public Team()
    {

    }

    [Key]
    [HiddenInput]
    [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
    public int Id { get; set; }

    [Display( Name = "Team's name" )]
    [Required( ErrorMessage = "Team name is required " )]
    [StringLength( 50 )]
    public string? Name { get; set; }

    [Display( Name = "Task descritpion" )]
    [StringLength( 250 )]
    [DataType( DataType.MultilineText )]
    public string? Description { get; set; }

    [Required]
    [Display( Name = "Assigment date" )]
    [DataType( DataType.Date )]
    public DateTime AssigmentDate { get; set; } = DateTime.UtcNow;

    [Required]
    [Display( Name = "Deadline of a task" )]
    [DataType( DataType.Date )]
    public DateTime DeadlineDate { get; set; }

    public int? ManagerId { get; set; }
    public Manager Manager { get; set; }    
    
    public List<Member> Members { get; set; }  




    //public List<Team_Member> Team_Members { get; set; }


    //[ForeignKey( "ManagerId")]
    //public int ManagerId { get; set; }
    //public Manager Manager { get; set; }


   




}
