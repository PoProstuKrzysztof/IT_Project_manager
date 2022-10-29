using System.ComponentModel.DataAnnotations;

namespace IT_Project_manager.Models
{
    public class Contact
    {
        [Required(ErrorMessage ="Please enter your name!")]
        public string? Name { get; set; }
        [RegularExpression(".+\\@.+\\.[a-z]{2,3}")]
        [Required(ErrorMessage ="Please enter your e-mail!")]
        public string? Email { get; set; }
        public string? Subject { get; set; }
        [MinLength(length:10, ErrorMessage ="Your message has to contain at least 10 characters!")]
        [Required(ErrorMessage ="Please enter your message!")]
        public string? Message { get; set; }
    }
}
