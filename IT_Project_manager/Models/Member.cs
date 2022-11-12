﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IT_Project_manager.Models
{



    public class Member
    {
        public static int m_counter = 0;
        [HiddenInput]
        public int Id { get; set; } 


        [Display( Name = "Name" )]
        [Required(ErrorMessage ="Name is required")]                
        public string? Name { get; set; }



        [Display( Name = "Surname" )]
        [Required(ErrorMessage = "Surname is required")]                   
        public string? Surname { get; set; }


        [Display(Name = "Email adress")]
        [EmailAddress(ErrorMessage ="Enter your email")]                
        public string? Email { get; set; }


        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]       
        public DateTime DateOfBirth { get; set; }



        public Member()
        {
            Id = Interlocked.Increment(ref m_counter);
            
        }

        public void Delete()
        {
            Id--;
            if (Id < 0)
            {
                Id = 0;
            }
        }
    }
}
