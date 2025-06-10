namespace Test2.Models;

public class BookAuthor
{
    public int IdAuthor { get; set; }
    public int IdBook { get; set; }
    public virtual Book Book { get; set; }
    public virtual Author Author { get; set; }
}