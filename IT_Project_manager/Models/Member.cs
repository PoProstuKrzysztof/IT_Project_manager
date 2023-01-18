using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Project_manager.Models
{
    [Table( "Members" )]
    public class Member
    {
        public Member()
        {
            Managers = new HashSet<Manager>();
            Teams = new HashSet<Team>();
        }

        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        [HiddenInput]
        public int Id { get; set; }

        [Display( Name = "Name" )]       
        public string? Name { get; set; }

        [Display( Name = "Surname" )]       
        public string? Surname { get; set; }

        [Display( Name = "Email adress" )]       
        public string? Email { get; set; }

        [Display( Name = "Date of birth" )]
        [DataType( DataType.Date )]
        public DateTime DateOfBirth { get; set; }



        public virtual ISet<Manager>? Managers { get; set; }

        public virtual ISet<Team>? Teams { get; set; }
    }
}