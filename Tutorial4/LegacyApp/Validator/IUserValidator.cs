namespace LegacyApp;

public interface IUserValidator
{
    bool Validate(string firstname, string lastname, string email, DateTime dateOfBirth);
}