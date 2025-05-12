namespace Warehouse.API.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public List<Product_Warehouse> ProductWarehouses { get; set; } = [];
    public List<Order> Orders { get; set; } = [];
}