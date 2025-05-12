using System.ComponentModel.DataAnnotations;

namespace Warehouse.API.Contracts.Requests;

public class ProductWarehouseRequest
{
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int WarehouseId { get; set; }
    [Required]
    public int Amount { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}