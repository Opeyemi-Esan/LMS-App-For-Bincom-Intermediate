using System.ComponentModel.DataAnnotations;
using System.Data;

namespace LMSAppFor_BincomIntermediate.Models
{
    public class LibraryUser
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public Role Role { get; set; } = Role.User; 

        public bool IsDeleted { get; set; } = false;

        public ICollection<BorrowedBookHistory> BorrowHistory { get; set; }
    }

    public enum Role
    {
        Admin,
        User
    }
}
