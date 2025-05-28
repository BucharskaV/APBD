using TripApp.Application.DTOs;
using TripApp.Core.Model;

namespace TripApp.Application.Repository;

public interface ITripRepository
{
    Task<PaginatedResult<Trip>> GetPaginatedTripsAsync(int page = 1, int pageSize = 10);
    Task<List<Trip>> GetAllTripsAsync();
    Task<bool> ClientExistsByPeselAsync(string pesel);
    Task<bool> ClientRegisteredByPeselAsync(string pesel, int idTrip);
    Task<bool> TripExistsAsync(int idTrip);
    Task<bool> TripInFuture(int idTrip);
    Task AssignClientToTripAsync(ClientRequestDto request);
}