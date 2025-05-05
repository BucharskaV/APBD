using Trips.API.Entities;

namespace Trips.API.Services.Interfaces;

public interface ITripService
{
    public Task<ICollection<Trip>> GetAllTripsWithCountriesAsync();
    public ValueTask<bool> RegisterClientToTripAsync(int clientId, int tripId);
    public Task<bool> DeleteReservationAsync(int clientId, int tripId);
}