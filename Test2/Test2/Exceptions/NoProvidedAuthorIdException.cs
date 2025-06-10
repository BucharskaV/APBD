namespace Test2.Exceptions;

public class NoProvidedAuthorIdException(int id) : Exception($"No provided author with id: {id}");