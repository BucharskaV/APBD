using Microsoft.EntityFrameworkCore;
using TripApp.Application.DTOs;
using TripApp.Application.Repository;
using TripApp.Core.Model;

namespace Trip.Infrastructure.Repository;

public class TripRepository(TripContext tripDbContext) : ITripRepository
{
    /// <summary>
    /// Retrieves a paginated list of trips based on the specified page number and page size.
    /// </summary>
    /// <param name="page">The current page number to retrieve. Defaults to 1.</param>
    /// <param name="pageSize">The number of items to include on each page. Defaults to 10.</param>
    /// <returns>A <see cref="PaginatedResult{Trip}"/> containing the paginated list of trips and metadata such as page size, page number, and total pages.</returns>
    public async Task<PaginatedResult<TripApp.Core.Model.Trip>> GetPaginatedTripsAsync(int page = 1, int pageSize = 10)
    {
        var tripsQuery = tripDbContext.Trips
            .Include(e => e.ClientTrips).ThenInclude(e => e.IdClientNavigation)
            .Include(e => e.IdCountries)
            .OrderByDescending(e => e.DateFrom);

        var tripsCount = await tripsQuery.CountAsync();
        var totalPages = tripsCount / pageSize;
        var trips = await tripsQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResult<TripApp.Core.Model.Trip>
        {
            PageSize = pageSize,
            PageNum = page,
            AllPages = totalPages,
            Data = trips
        };
    }

    public async Task<List<TripApp.Core.Model.Trip>> GetAllTripsAsync()
    {
        return await tripDbContext.Trips
            .Include(e => e.ClientTrips).ThenInclude(e => e.IdClientNavigation)
            .Include(e => e.IdCountries)
            .OrderBy(e => e.DateFrom)
            .ToListAsync();
    }

    public async Task<bool> ClientExistsByPeselAsync(string pesel)
    {
        var client = await tripDbContext.Clients.FirstOrDefaultAsync(x => x.Pesel == pesel);
        return client is not null;
    }

    public async Task<bool> ClientRegisteredByPeselAsync(string pesel, int idTrip)
    {
        var client = await tripDbContext.Clients.FirstOrDefaultAsync(x => x.Pesel == pesel);
        return await tripDbContext.ClientTrips.AnyAsync(ct => client != null && ct.IdClient == client.IdClient && ct.IdTrip == idTrip);
    }

    public async Task<bool> TripExistsAsync(int idTrip)
    {
        var trip = await tripDbContext.Trips.FirstOrDefaultAsync(x => x.IdTrip == idTrip);
        return trip is not null;
    }

    public async Task<bool> TripInFuture(int idTrip)
    {
        var trip = await tripDbContext.Trips.FirstOrDefaultAsync(x => x.IdTrip == idTrip);
        return trip != null && trip.DateFrom > DateTime.Now;
    }

    public async Task AssignClientToTripAsync(ClientRequestDto request)
    {
        var client = new Client
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Telephone = request.Telephone,
            Pesel = request.Pesel
        };
        tripDbContext.Clients.Add(client);
        await tripDbContext.SaveChangesAsync();
        var clientTrip = new ClientTrip
        {
            IdClient = client.IdClient,
            IdTrip = request.IdTrip,
            RegisteredAt = DateTime.Now,
            PaymentDate = request.PaymentDate
        };
        tripDbContext.ClientTrips.Add(clientTrip);
        await tripDbContext.SaveChangesAsync();
    }
}