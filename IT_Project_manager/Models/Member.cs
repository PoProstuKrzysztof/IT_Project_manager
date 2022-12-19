using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Project_manager.Models
{
    [Table( "Members" )]
    public class Member
    {
      
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput]
        public int Id { get; set; }

        [Display( Name = "Name" )]
        [Required( ErrorMessage = "Name is required" )]
        public string? Name { get; set; }

        [Display( Name = "Surname" )]
        [Required( ErrorMessage = "Surname is required" )]
        public string? Surname { get; set; }

        [Display( Name = "Email adress" )]
        [EmailAddress( ErrorMessage = "Enter your email" )]
        public string? Email { get; set; }

        [Display( Name = "Date of birth" )]
        [DataType( DataType.Date )]
        public DateTime DateOfBirth { get; set; }

        public ISet<Manager>? Managers { get; set; }

        public Member()
        {
            Managers = new HashSet<Manager>();
        }

    }
}