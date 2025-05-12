namespace Warehouse.API.Entities;

public class Order
{
    public int Id { get; set; }
    public Product Product { get; set; } = null!;
    public int Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? FulfilledAt { get; set; }
    public List<Product_Warehouse> ProductWarehouses { get; set; } = [];
}