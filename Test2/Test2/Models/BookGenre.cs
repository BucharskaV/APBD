namespace Test2.Models;

public class BookGenre
{
    public int IdGenre { get; set; }
    public int IdBook { get; set; }
    public virtual Book Book { get; set; }
    public virtual Genre Genre { get; set; }
}