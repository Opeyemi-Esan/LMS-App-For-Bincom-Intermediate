using LMSAppFor_BincomIntermediate.Models;
using System.ComponentModel.DataAnnotations;

namespace LMSAppFor_BincomIntermediate.Dtos
{
    public class LibraryUserRegDto
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required, Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public Role Role { get; set; } 
    }
}
