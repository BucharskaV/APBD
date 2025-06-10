namespace Test2.Contracts.Responces;

public class BookResponse
{
    public BookDto Book { get; set; }
    public PublishingHouseDto PublishingHouse { get; set; }
    public List<AuthorDto> Authors { get; set; } = [];
    public List<GenreDto> Genres { get; set; } = [];
}

public class BookDto
{
    public int IdBook { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
}

public class PublishingHouseDto
{
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}

public class AuthorDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public class GenreDto
{
    public string Name { get; set; } = string.Empty;
}