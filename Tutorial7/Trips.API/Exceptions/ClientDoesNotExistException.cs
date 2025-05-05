namespace Trips.API.Exceptions;

public class ClientDoesNotExistException(int id) : Exception($"Client with {id} not found or the client does not have trips");