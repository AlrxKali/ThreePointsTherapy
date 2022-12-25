using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class UserRegistryDTO
    {
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
