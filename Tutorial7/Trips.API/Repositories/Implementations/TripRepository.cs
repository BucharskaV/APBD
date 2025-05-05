using Microsoft.Data.SqlClient;
using Trips.API.Entities;
using Trips.API.Repositories.Interfaces;

namespace Trips.API.Repositories.Implementations;

public class TripRepository : ITripRepository
{
    private readonly string? _connectionString;

    public TripRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    // Return trip details including ID, name, description, date range, and maximum number of participants
    //Include country information for each trip
    public async Task<List<Trip>> GetTripsAsync()
    {
        Dictionary<int, Trip>? trips = new Dictionary<int, Trip>();
        string query = @"SELECT t.IdTrip, t.Name, t.Description, t.DateFrom, t.DateTo, t.MaxPeople, c.IdCountry, c.Name
                             FROM Country_Trip as ct 
                                 JOIN Country as c on ct.IdCountry = c.IdCountry 
                                 JOIN Trip as t on ct.IdTrip = t.IdTrip";
        
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            await connection.OpenAsync();
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var id = reader.GetInt32(0);
                    var trip = new Trip
                    {
                        Id = id,
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        DateFrom = reader.GetDateTime(3),
                        DateTo = reader.GetDateTime(4),
                        MaxPeople = reader.GetInt32(5),
                        Countries = []
                    };
                    trips.Add(id, trip);
                    if (!reader.IsDBNull(6))
                    {
                        var countryId = reader.GetInt32(6);
                        Country country = new()
                        {
                            Id = countryId,
                            Name = reader.GetString(7),
                        };
                        trip.Countries.Add(country);
                    }
                }
            }
        }
        return trips.Values.ToList();
    }

    // Include trip details and registration/payment information
    //Handle cases where client doesn't exist or has no trips
    public async Task<Trip?> GetTripByIdAsync(int id)
    {
        var tripExists = await TripExistsAsync(id);
        if (!tripExists)
            return null;
        Dictionary<int, Trip>? trips = new Dictionary<int, Trip>();
        string query = @"SELECT t.IdTrip, t.Name, t.Description, t.DateFrom, t.DateTo, t.MaxPeople, c.IdCountry, c.Name
                             FROM Country_Trip as ct 
                                 JOIN Country as c on ct.IdCountry = c.IdCountry 
                                 JOIN Trip as t on ct.IdTrip = t.IdTrip
                                 WHERE ct.IdTrip = @id";
        
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@id", id);
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var idt = reader.GetInt32(0);
                    var trip = new Trip
                    {
                        Id = idt,
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        DateFrom = reader.GetDateTime(3),
                        DateTo = reader.GetDateTime(4),
                        MaxPeople = reader.GetInt32(5),
                        Countries = []
                    };
                    trips.Add(idt, trip);
                    if (!reader.IsDBNull(6))
                    {
                        var countryId = reader.GetInt32(6);
                        Country country = new()
                        {
                            Id = countryId,
                            Name = reader.GetString(7),
                        };
                        trip.Countries.Add(country);
                    }
                }
            }
        }
        return trips.Values.FirstOrDefault();
    }
    
    //Checks if the trip exists by Id
    public async Task<bool> TripExistsAsync(int id)
    {
        if (id <= 0) return false;

        string query = @"SELECT 1 FROM Trip WHERE Trip.IdTrip = @id";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@id", id);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result != null && Convert.ToInt32(result) == 1;
        }
    }
}