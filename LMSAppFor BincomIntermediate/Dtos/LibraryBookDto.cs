namespace LMSAppFor_BincomIntermediate.Dtos
{
    public class LibraryBookDto
    {
        public string Title { get; set; }
        public List<Author> Authors { get; set; }
    }

    public class Author
    {
        public string Name { get; set; }
    }
}
