using System.Runtime.CompilerServices;

namespace Animals.Api.Model;

public class Visit
{
    public required int Id { get; set; }
    public required DateTime Date { get; set; }
    public required int AnimalId { get; set; }
    public string? Description { get; set; }
    public required double Price { get; set; }
}