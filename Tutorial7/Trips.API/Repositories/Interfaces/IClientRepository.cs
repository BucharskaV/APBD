using Trips.API.Entities;

namespace Trips.API.Repositories.Interfaces;

public interface IClientRepository
{
    public Task<bool> ClientExistsAsync(int id);
    public Task<Client?> GetClientByIdAsync(int id);
    public Task<List<ClientTrip>?> GetClientTripsAsync(int id);
    public Task<bool> CreateClientTripAsync(ClientTrip clientTrip);
    public Task<Client> CreateClientAsync(Client client);
}