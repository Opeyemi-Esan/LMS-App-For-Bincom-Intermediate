using System.ComponentModel.DataAnnotations;

namespace LMSAppFor_BincomIntermediate.Models
{
    public class BorrowedBookHistory
    {
        [Key]
        public Guid Id { get; set; } 

        [Required]
        public Guid UserId { get; set; }
        public LibraryUser User { get; set; }

        [Required]
        public Guid BookId { get; set; }
        public Book Book { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; } 

        public DateTime? ReturnDate { get; set; }

        public bool IsReturned { get; set; } = false;
    }
}
