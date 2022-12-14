using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Project_manager.Models;

[Table( "Managers" )]
public class Manager
{
    [Key]
    [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
    public int Id { get; set; }

    [Required]
    [Column( "first_name" )]
    [StringLength( 50 )]
    public string Name { get; set; }

    [Required]
    [Column( "surname" )]
    [StringLength( 30 )]
    public string Surname { get; set; }

    [Column( "telephone" )]
    [DataType( DataType.PhoneNumber )]
    public string Telephone { get; set; }

    public ISet<Member> Members { get; set; }
}