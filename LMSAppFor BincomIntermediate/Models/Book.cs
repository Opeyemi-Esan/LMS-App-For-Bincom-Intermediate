using System.ComponentModel.DataAnnotations;

namespace LMSAppFor_BincomIntermediate.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Required, MaxLength(150)]
        public string Author { get; set; }

        [MaxLength(13)]
        public string ISBN { get; set; }

        [Required]
        public int TotalCopies { get; set; }

        [Required]
        public int AvailableCopies { get; set; }

        public ICollection<BorrowedBookHistory> BorrowHistories { get; set; }
    }
}
