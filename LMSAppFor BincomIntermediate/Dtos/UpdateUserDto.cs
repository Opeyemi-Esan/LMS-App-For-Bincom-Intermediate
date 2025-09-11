using LMSAppFor_BincomIntermediate.Models;
using System.ComponentModel.DataAnnotations;

namespace LMSAppFor_BincomIntermediate.Dtos
{
    public class UpdateUserDto
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
