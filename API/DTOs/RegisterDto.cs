using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required] //this is validation to check null fields
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}