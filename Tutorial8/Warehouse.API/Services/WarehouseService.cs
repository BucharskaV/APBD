﻿using Warehouse.API.Contracts.Requests;
using Warehouse.API.Exceptions;
using Warehouse.API.Repositories;
using Warehouse.API.Repositories.Abstractions;

namespace Warehouse.API.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public WarehouseService(IWarehouseRepository warehouseRepository, IUnitOfWork unitOfWork)
    {
        _warehouseRepository = warehouseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> RegisterProductWarehouseAsync(ProductWarehouseRequest request, CancellationToken token = default)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            if (request.Amount <= 0)
                throw new AmountMustBeGreaterThanZeroException();
            if (!await _warehouseRepository.ProductExistsAsync(request.IdProduct, token))
                throw new ProductDoesNotExistException();
            if (!await _warehouseRepository.WarehouseExistsAsync(request.IdWarehouse, token))
                throw new WarehouseDoesNotExistException();
            
            var orderId = await _warehouseRepository.GetOrderIdAsync(request.IdProduct, request.Amount, request.CreatedAt, token);
            if(orderId == null)
                throw new AppropriateOrderNotFoundException();
            if(await _warehouseRepository.OrderCompleteAsync(orderId.Value, token))
                throw new OrderIsCompletedException();
            
            await _warehouseRepository.UpdateOrderFulfilledAsync(orderId.Value,  token);
            
            var priceProduct = await _warehouseRepository.GetPriceAsync(request.IdProduct, token);
            var totalPrice = priceProduct * request.Amount;
            var id = await _warehouseRepository.InsertProductWarehouseAsync(request.IdProduct, request.IdWarehouse, orderId.Value, request.Amount, totalPrice, token);
            await _unitOfWork.CommitTransactionAsync();
            return id;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<int> RegisterProductWarehouseWithProcedureAsync(ProductWarehouseRequest request, CancellationToken token)
    {
        return await _warehouseRepository.AddProductToWarehouseUsingProcedureAsync(request, token);
    }
}