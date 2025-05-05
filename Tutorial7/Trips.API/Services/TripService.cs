using Trips.API.Entities;
using Trips.API.Exceptions;
using Trips.API.Repositories.Interfaces;

namespace Trips.API.Services;

public class TripService
{
    private readonly ITripRepository _tripRepository;
    private readonly IClientRepository _clientRepository;

    public TripService(ITripRepository tripRepository, IClientRepository clientRepository)
    {
        _tripRepository = tripRepository;
        _clientRepository = clientRepository;
    }

    public async Task<ICollection<Trip>> GetAllTripsWithCountriesAsync()
    {
        return await _tripRepository.GetTripsAsync();
    }

    public async ValueTask<bool> RegisterClientToTripAsync(int clientId, int tripId)
    {
        var client = await _clientRepository.GetClientByIdAsync(clientId);
        if (client is null)
            throw new ClientDoesNotExistException(clientId);
        var trip = await _tripRepository.GetTripByIdAsync(tripId);
        if (trip is null)
            throw new TripDoesNotExistException(clientId);
        if (trip.Clients.Count + 1 > trip.MaxPeople)
            throw new MaxParticipantNumberReachedException();
        var clientTrip = new ClientTrip
        {
            Trip = trip,
            Client = client,
            PaymentDate = null,
            RegisteredAt = DateTime.UtcNow.Year * 10000 + DateTime.UtcNow.Month * 100 + DateTime.UtcNow.Day
        };
        var result = await _clientRepository.CreateClientTripAsync(clientTrip);
        return result;
    }
    
    public async Task<bool> DeleteReservationAsync(int clientId, int tripId)
    {
        if (!await _tripRepository.ReservationExistsAsync(clientId, tripId))
            throw new ReservationDoesNotExistException();

        await _tripRepository.DeleteReservationAsync(clientId, tripId);
        return true;
    }
}
