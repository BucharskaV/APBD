using TripApp.Application.DTOs;
using TripApp.Application.Exceptions;
using TripApp.Application.Mappers;
using TripApp.Application.Repository;
using TripApp.Application.Services.Interfaces;
using TripApp.Core.Model;

namespace TripApp.Application.Services;

public class TripService(ITripRepository tripRepository) : ITripService
{
    public async Task<PaginatedResult<GetTripDto>> GetPaginatedTripsAsync(int page = 1, int pageSize = 10)
    {
        if (page < 1) page = 1;
        if (pageSize < 10) pageSize = 10;
        var result = await tripRepository.GetPaginatedTripsAsync(page, pageSize);

        var mappedTrips = new PaginatedResult<GetTripDto>
        {
            AllPages = result.AllPages,
            Data = result.Data.Select(trip => trip.MapToGetTripDto()).ToList(),
            PageNum = result.PageNum,
            PageSize = result.PageSize
        };

        return mappedTrips;
    }

    public async Task<IEnumerable<GetTripDto>> GetAllTripsAsync()
    {
        var trips = await tripRepository.GetAllTripsAsync();
        var mappedTrips = trips.Select(trip => trip.MapToGetTripDto()).ToList();
        return mappedTrips;
    }

    public async Task AssignClientToTripAsync(ClientRequestDto request)
    {
        if (await tripRepository.ClientExistsByPeselAsync(request.Pesel))
            throw new ClientExceptions.ClientExistsByPeselException(request.Pesel);
        
        if (await tripRepository.ClientRegisteredByPeselAsync(request.Pesel, request.IdTrip))
            throw new ClientExceptions.ClientRegisteredByPeselException(request.Pesel);
        
        if(!await tripRepository.TripExistsAsync(request.IdTrip))
            throw new TripExceptions.TripDoesNotExistException(request.IdTrip);
        
        if(!await tripRepository.TripInFuture(request.IdTrip))
            throw new TripExceptions.TripHasAlreadyOccuredException(request.IdTrip);

        await tripRepository.AssignClientToTripAsync(request);
    }
}