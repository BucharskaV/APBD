namespace Test.Exceptions;

public class NoMemberWithProvidedIdException(int id) : Exception($"No member with provided id {id} was found.");