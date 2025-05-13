namespace Test.Exceptions;

public class NoProjectWithProvidedIdException(int id) : Exception($"No project with provided id {id} was found.");