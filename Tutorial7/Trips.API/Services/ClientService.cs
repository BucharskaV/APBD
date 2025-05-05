using Trips.API.Contracts.Requests;
using Trips.API.Entities;
using Trips.API.Repositories.Interfaces;

namespace Trips.API.Services;

public class ClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<ICollection<ClientTrip>?> GetAllClientTripsAsync(int clientId)
    {
        return await _clientRepository.GetClientTripsAsync(clientId);
    }

    public async Task<int> CreateClientAsync(CreateClientRequest client)
    {
        var newClient = new Client
        {
            Telephone = client.Telephone,
            FirstName = client.FirstName,
            LastName = client.LastName,
            Email = client.Email,
            Pesel = client.Pesel
        };
        var result = await _clientRepository.CreateClientAsync(newClient);
        return result.Id;
    }
}