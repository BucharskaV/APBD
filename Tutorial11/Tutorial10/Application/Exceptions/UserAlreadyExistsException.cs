namespace Tutorial10.Application.Exceptions;

public class UserAlreadyExistsException(string username) : Exception($"User {username} already exists!");