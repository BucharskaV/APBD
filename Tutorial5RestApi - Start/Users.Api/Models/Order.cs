namespace Users.Api.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public Dictionary<int, string> Products { get; set; } = [];
}