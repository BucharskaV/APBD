using Warehouse.API.Contracts.Requests;

namespace Warehouse.API.Services;

public interface IWarehouseService
{
    public Task<int> RegisterProductWarehouseAsync(ProductWarehouseRequest request, CancellationToken token = default);

    public Task<int> RegisterProductWarehouseWithProcedureAsync(ProductWarehouseRequest request, CancellationToken token);
}