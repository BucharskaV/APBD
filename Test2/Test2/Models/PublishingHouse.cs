namespace Test2.Models;

public class PublishingHouse
{
    public int IdPublishingHouse { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public virtual ICollection<Book> Books { get; set; } = [];
}