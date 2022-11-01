using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IT_Project_manager.Models
{



    public class Member
    {
        public static int m_counter = 0;
        [HiddenInput]
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string? Surname { get; set; }
        [EmailAddress(ErrorMessage ="Enter your email")]
        public string? Email { get; set; }
        public int BirthDate { get; set; }


        public Member()
        {
            Id = Interlocked.Increment(ref m_counter);
        }
    }
}
