using Warehouse.API.Contracts.Requests;
using Warehouse.API.Entities;

namespace Warehouse.API.Repositories.Abstractions;

public interface IWarehouseRepository
{
    public Task<bool> ProductExistsAsync(int productId, CancellationToken token = default);
    public Task<bool> WarehouseExistsAsync(int warehouseId, CancellationToken token = default);
    public Task<int?> GetOrderIdAsync(int productId, int amount, DateTime requestCreatedAt, CancellationToken token = default);
    
    public Task<bool> OrderCompleteAsync(int orderId, CancellationToken token = default);
    
    public Task UpdateOrderFulfilledAsync(int orderId, CancellationToken token = default);
    public Task<int> InsertProductWarehouseAsync(int productId, int warehouseId, int orderId, int amount, decimal price, CancellationToken token = default);
    
    public Task<decimal> GetPriceAsync(int productId, CancellationToken token = default);
    
    public Task<int> AddProductToWarehouseUsingProcedureAsync(ProductWarehouseRequest request, CancellationToken token);
}