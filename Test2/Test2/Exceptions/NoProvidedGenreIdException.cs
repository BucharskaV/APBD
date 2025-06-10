namespace Test2.Exceptions;

public class NoProvidedGenreIdException(int id) : Exception($"No provided genre with id: {id}");