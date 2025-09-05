using LMSAppFor_BincomIntermediate.Models;
using System.ComponentModel.DataAnnotations;

namespace LMSAppFor_BincomIntermediate.Dtos
{
    public class BorrowedBookHistoryDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid BookId { get; set; }
        public string BookTitle { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
    }
}
