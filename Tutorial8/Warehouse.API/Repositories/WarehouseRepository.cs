using System.Data;
using Warehouse.API.Contracts.Requests;
using Warehouse.API.Entities;
using Warehouse.API.Repositories.Abstractions;

namespace Warehouse.API.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public WarehouseRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<bool> ProductExistsAsync(int productId, CancellationToken token = default)
    {
        const string query = @"SELECT 
                                 IIF(EXISTS (SELECT 1 FROM Product 
                                         WHERE Product.IdProduct = @productId), 1, 0);   
                                ";
        var connection = await _unitOfWork.GetConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = query;
        command.Transaction = _unitOfWork.Transaction;
        command.Parameters.AddWithValue("@productId", productId);
        var result = Convert.ToInt32(await command.ExecuteScalarAsync(token));
        return result == 1;
    }

    public async Task<bool> WarehouseExistsAsync(int warehouseId, CancellationToken token = default)
    {
        const string query = @"SELECT 
                                 IIF(EXISTS (SELECT 1 FROM Warehouse 
                                         WHERE Warehouse.IdWarehouse = @warehouseId), 1, 0);   
                                ";
        
        var connection = await _unitOfWork.GetConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = query;
        command.Transaction = _unitOfWork.Transaction;
        command.Parameters.AddWithValue("@warehouseId", warehouseId);
        
        var result = Convert.ToInt32(await command.ExecuteScalarAsync(token));
        return result == 1;
    }

    public async Task<int?> GetOrderIdAsync(int productId, int amount, DateTime requestCreatedAt, CancellationToken token = default)
    {
         const string query = @"
            SELECT o.IdOrder
            FROM [Order] o
            WHERE o.IdProduct = @productId AND o.Amount = @amount AND o.CreatedAt > @requestCreatedAt
        ";
        var connection = await _unitOfWork.GetConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = query;
        command.Transaction = _unitOfWork.Transaction;
        command.Parameters.AddWithValue("@productId", productId);
        command.Parameters.AddWithValue("@amount", amount);
        command.Parameters.AddWithValue("@requestCreatedAt", requestCreatedAt);

        var result = await command.ExecuteScalarAsync(token);
        return result != null ? Convert.ToInt32(result) : (int?)null;
    }

    public async Task<bool> OrderCompleteAsync(int orderId, CancellationToken token = default)
    {
        const string query = @"SELECT 1 FROM Product_Warehouse WHERE IdOrder = @orderId";
        
        var connection = await _unitOfWork.GetConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = query;
        command.Transaction = _unitOfWork.Transaction;
        command.Parameters.AddWithValue("@orderId", orderId);
        
        var result = Convert.ToInt32(await command.ExecuteScalarAsync(token));
        return result == 1;
    }

    public async Task UpdateOrderFulfilledAsync(int orderId, CancellationToken token = default)
    {
        const string query = @"UPDATE [Order] SET FulfilledAt = GETDATE() WHERE IdOrder = @orderId";
        
        var connection = await _unitOfWork.GetConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = query;
        command.Transaction = _unitOfWork.Transaction;
        command.Parameters.AddWithValue("@orderId", orderId);
        await command.ExecuteNonQueryAsync(token);
    }

    public async Task<int> InsertProductWarehouseAsync(int productId, int warehouseId, int orderId, int amount, decimal price,
        CancellationToken token = default)
    {
        const string query = @"
            INSERT INTO Product_Warehouse(IdProduct, IdWarehouse, IdOrder, Amount, Price, CreatedAt)
            VALUES (@productId, @warehouseId, @orderId, @amount, @price, GETDATE());
            SELECT SCOPE_IDENTITY();
                                ";
        
        var connection = await _unitOfWork.GetConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = query;
        command.Transaction = _unitOfWork.Transaction;
        command.Parameters.AddWithValue("@productId", productId);
        command.Parameters.AddWithValue("@warehouseId", warehouseId);
        command.Parameters.AddWithValue("@orderId", orderId);
        command.Parameters.AddWithValue("@amount", amount);
        command.Parameters.AddWithValue("@price", price);
        
        var result = await command.ExecuteScalarAsync();
        return Convert.ToInt32(result);
    }

    public async Task<decimal> GetPriceAsync(int productId, CancellationToken token = default)
    {
        const string query = @"SELECT Price FROM Product WHERE IdProduct = @productId";
        var connection = await _unitOfWork.GetConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = query;
        command.Transaction = _unitOfWork.Transaction;
        command.Parameters.AddWithValue("@productId", productId);

        var result = await command.ExecuteScalarAsync(token);
        return (decimal)(result != null ? Convert.ToDecimal(result) : (decimal?)null);
    }

    public async Task<int> AddProductToWarehouseUsingProcedureAsync(ProductWarehouseRequest request, CancellationToken token)
    {
        var connection = await _unitOfWork.GetConnectionAsync();
        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync(token);
        await using var command = connection.CreateCommand();
        command.CommandText = "AddProductToWarehouse";
        command.CommandType = CommandType.StoredProcedure;
        command.Transaction = _unitOfWork.Transaction;
        command.Parameters.AddWithValue("@IdProduct", request.IdProduct);
        command.Parameters.AddWithValue("@IdWarehouse", request.IdWarehouse);
        command.Parameters.AddWithValue("@Amount", request.Amount);
        command.Parameters.AddWithValue("@CreatedAt", request.CreatedAt);

        var result = await command.ExecuteScalarAsync(token);
        return Convert.ToInt32(result);
    }
}