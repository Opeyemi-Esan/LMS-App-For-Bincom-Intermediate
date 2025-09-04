using LMSAppFor_BincomIntermediate.Models;

namespace LMSAppFor_BincomIntermediate.Dtos
{
    public class AuthorGroupedBooksDto
    {
        public string Author { get; set; }
        public List<Book> Books { get; set; }
    }
}
