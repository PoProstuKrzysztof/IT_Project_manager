using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Project_manager.Models;

[Table( "Teams" )]
public class Team
{
    public Team()
    {
        Members = new HashSet<Member>();
        Managers = new HashSet<Manager>();
    }

    [Key]
    [HiddenInput]
    [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
    public int Id { get; set; }

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

    public virtual ISet<Manager>? Managers { get; set; }

    public virtual ISet<Member>? Members { get; set; }
}