namespace Trips.API.Exceptions;

public class TripDoesNotExistException(int id) : Exception($"Trip with id {id} does not exist");