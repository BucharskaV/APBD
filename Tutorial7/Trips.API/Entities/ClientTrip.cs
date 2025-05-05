namespace Trips.API.Entities;

public class ClientTrip
{
    public Client Client { get; set; }
    public Trip Trip { get; set; }
    public int RegisteredAt { get; set; }
    public int? PaymentDate { get; set; }
}