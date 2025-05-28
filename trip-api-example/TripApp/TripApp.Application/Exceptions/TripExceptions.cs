namespace TripApp.Application.Exceptions;

public class TripExceptions
{
    public class TripDoesNotExistException(int trip) 
        : Exception(message:$"Trip {trip} does not exist.");
    public class TripHasAlreadyOccuredException(int trip) 
        : Exception(message:$"Trip {trip} already occured.");
}