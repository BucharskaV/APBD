using Warehouse.API.Contracts.Requests;

namespace Warehouse.API.Services;

public interface IWarehouseService
{
    public Task<int> RegisterProductWarehouseAsync(ProductWarehouseRequest request, CancellationToken token = default);
}