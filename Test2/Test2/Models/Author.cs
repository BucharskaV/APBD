namespace Test2.Models;

public class Author
{
    public int IdAuthor { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public virtual ICollection<BookAuthor> BookAuthors { get; set; } = [];
}