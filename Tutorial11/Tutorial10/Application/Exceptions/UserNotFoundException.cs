namespace Tutorial10.Application.Exceptions;

public class UserNotFoundException(string login) : Exception($"User {login} not found");