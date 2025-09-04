using System.ComponentModel.DataAnnotations;

namespace LMSAppFor_BincomIntermediate.Dtos
{
    public class BookDto
    {
        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Required, MaxLength(150)]
        public string Author { get; set; }

        [MaxLength(13)]
        public string ISBN { get; set; }

        [Required]
        public int TotalCopies { get; set; }
    }
}
