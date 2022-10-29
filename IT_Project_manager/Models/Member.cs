using System.ComponentModel.DataAnnotations;

namespace IT_Project_manager.Models
{
    public class Member
    {
        [Required(ErrorMessage ="Please enter your name!")]
        public string Name { get; set; }
        [RegularExpression]
    }
}
