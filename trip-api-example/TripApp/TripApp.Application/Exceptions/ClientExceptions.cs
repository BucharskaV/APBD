namespace TripApp.Application.Exceptions;

public static class ClientExceptions
{
    public class ClientHasTripsException() 
        : InvalidOperationException("Client has trips.");
    
    public class ClientNotFoundException(string clientId) 
        : BaseExceptions.NotFoundException($"Client not found with client id {clientId}");
    
    public class ClientExistsByPeselException(string pesel) 
        : Exception(message:$"Client with Pesel {pesel} already exists.");
    public class ClientRegisteredByPeselException(string pesel) 
        : Exception(message:$"Client with Pesel {pesel} is registered for given trip.");
}