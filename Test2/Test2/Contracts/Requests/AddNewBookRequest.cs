namespace Test2.Contracts.Requests;

public class AddNewBookRequest
{
    public int IdPublishingHouse { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string NameBook { get; set; } = string.Empty;
    public int IdBook { get; set; }
    public List<int> AuthorIds { get; set; } = [];
    public List<int> GenreIds { get; set; } = [];
}