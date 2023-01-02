using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Project_manager.Models;

[Table( "Managers" )]
public class Manager
{
    public Manager()
    {
        Members = new HashSet<Member>();
        Teams = new HashSet<Team>();
    }

    [Key]
    [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
    [HiddenInput]
    public int Id { get; set; }

    [Required]
    [Column( "Name" )]
    [StringLength( 50 )]
    public string Name { get; set; }

    [Required]
    [Column( "Surname" )]
    [StringLength( 30 )]
    public string Surname { get; set; }

    [Column( "Telephone" )]
    [StringLength( 12, MinimumLength = 9 )]
    [DataType( DataType.PhoneNumber )]
    public string Telephone { get; set; }

    public virtual ISet<Member> Members { get; set; }

    public virtual ISet<Team> Teams { get; set; }
}