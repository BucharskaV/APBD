using Microsoft.Data.SqlClient;
using Trips.API.Entities;
using Trips.API.Repositories.Interfaces;

namespace Trips.API.Repositories.Implementations;

public class ClientRepository : IClientRepository
{
    private readonly string? _connectionString;

    public ClientRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }
    
    //Checks if the Client exists by ID
    public async Task<bool> ClientExistsAsync(int id)
    {
        if (id <= 0) return false;

        string query = @"SELECT 1 FROM Client WHERE Client.IdClient = @id";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@id", id);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result != null && Convert.ToInt32(result) == 1;
        }
    }

    //Retrieve Client exists by ID
    public async Task<Client?> GetClientByIdAsync(int id)
    {
        Client? client = null;
        var clientExists = await ClientExistsAsync(id);
        if (!clientExists)
            return client;
        Dictionary<int, Client>? clients = new Dictionary<int, Client>();
        string query = @"SELECT IdClient, FirstName, LastName, Email, Telephone, Pesel
                             FROM Client
                             WHERE IdClient = @id";
        
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@id", id);
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    client = new Client
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Email = reader.GetString(3),
                        Pesel = reader.GetString(4),
                        Telephone = reader.GetString(5),
                        Trips = null
                    };
                }
            }
        }
        return client;
    }

    //Retrieve reservation
    public async Task<List<ClientTrip>?> GetClientTripsAsync(int id)
{
    var client = await GetClientByIdAsync(id);
    if (client is null)
        return null;

    Dictionary<int, ClientTrip> clientTrips = new Dictionary<int, ClientTrip>();

    string query = @"SELECT T.IdTrip, T.Name, T.DateFrom, T.DateTo, T.Description, T.MaxPeople, CT.PaymentDate, CT.RegisteredAt, C.IdCountry, C.Name
                     FROM Client_Trip as CT 
                     JOIN Trip T on T.IdTrip = CT.IdTrip
                     JOIN Country_Trip as CT2 on CT2.IdTrip = T.IdTrip
                     JOIN Country as C on C.IdCountry = CT2.IdCountry
                     WHERE CT.IdClient = @id";

    using (SqlConnection connection = new SqlConnection(_connectionString))
    using (SqlCommand command = new SqlCommand(query, connection))
    {
        await connection.OpenAsync();
        command.Parameters.AddWithValue("@id", id);
        using (SqlDataReader reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                var tripId = reader.GetInt32(0);
                var countryId = reader.GetInt32(8);
                var countryName = reader.GetString(9);

                if (!clientTrips.TryGetValue(tripId, out var clientTrip))
                {
                    clientTrip = new ClientTrip
                    {
                        Client = client,
                        Trip = new Trip
                        {
                            Id = tripId,
                            Name = reader.GetString(1),
                            DateFrom = reader.GetDateTime(2),
                            DateTo = reader.GetDateTime(3),
                            Description = reader.GetString(4),
                            MaxPeople = reader.GetInt32(5),
                            Countries = []
                        },
                        PaymentDate = reader.IsDBNull(6) ? null : reader.GetInt32(6),
                        RegisteredAt = reader.GetInt32(7)
                    };
                    clientTrips.Add(tripId, clientTrip);
                }
                if (clientTrip.Trip.Countries.All(c => c.Id != countryId))
                {
                    clientTrip.Trip.Countries.Add(new Country
                    {
                        Id = countryId,
                        Name = countryName
                    });
                }
            }
        }
    }
    return clientTrips.Values.ToList();
}


    //Create a reservation
    public async Task<bool> CreateClientTripAsync(ClientTrip clientTrip)
    { 
        string query = @"
                             INSERT INTO Client_Trip (IdClient, IdTrip, RegisteredAt, PaymentDate)
                             VALUES (@idClient, @idTrip, @registeredAt, @paymentDate)
                             ";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@idClient", clientTrip.Client?.Id);
            command.Parameters.AddWithValue("@idTrip", clientTrip.Trip?.Id);
            command.Parameters.AddWithValue("@registeredAt", clientTrip.RegisteredAt);
            command.Parameters.AddWithValue("@paymentDate",
                clientTrip.PaymentDate.HasValue ? clientTrip.PaymentDate.Value : DBNull.Value);

            await connection.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }
    }

    //Create a new client
    public async Task<Client> CreateClientAsync(Client client)
    {
        string query = @"
                             INSERT INTO Client(FirstName, LastName, Email, Telephone, Pesel)
                             VALUES (@FirstName, @LastName, @Email, @Telephone, @Pesel)
                             SELECT SCOPE_IDENTITY();
                             ";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@FirstName", client.FirstName);
            command.Parameters.AddWithValue("@LastName", client.LastName);
            command.Parameters.AddWithValue("@Email", client.Email);
            command.Parameters.AddWithValue("@Telephone", client.Telephone);
            command.Parameters.AddWithValue("@Pesel", client.Pesel);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            client.Id = Convert.ToInt32(result);
            return client;
        }
    }
}