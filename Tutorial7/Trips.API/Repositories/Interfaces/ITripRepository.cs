using Trips.API.Entities;

namespace Trips.API.Repositories.Interfaces;

public interface ITripRepository
{
    public Task<List<Trip>> GetTripsAsync();
    public Task<Trip?> GetTripByIdAsync(int id);
    public Task<bool> TripExistsAsync(int id);
    public Task DeleteReservationAsync(int clientId, int tripId);
    public Task<bool> ReservationExistsAsync(int clientId, int tripId);
}