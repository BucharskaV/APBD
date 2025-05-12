namespace Warehouse.API.Entities;

public class Warehouse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public List<Product_Warehouse> ProductWarehouses { get; set; } = [];
}