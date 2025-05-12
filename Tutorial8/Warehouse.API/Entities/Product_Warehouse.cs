namespace Warehouse.API.Entities;

public class Product_Warehouse
{
    public int Id { get; set; }
    public Warehouse Warehouse { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public Order Order { get; set; } = null!;
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
}