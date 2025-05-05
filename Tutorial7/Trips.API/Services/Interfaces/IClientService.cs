using Trips.API.Contracts.Requests;
using Trips.API.Entities;

namespace Trips.API.Services.Interfaces;

public interface IClientService
{
    public Task<ICollection<ClientTrip>?> GetAllClientTripsAsync(int clientId);
    public Task<int> CreateClientAsync(CreateClientRequest client);
}