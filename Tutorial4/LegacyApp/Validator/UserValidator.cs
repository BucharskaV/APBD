namespace LegacyApp;

public class UserValidator : IUserValidator
{
    
    public bool Validate(string firstname, string lastname, string email, DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        int age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
        
        if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname))
        {
            return false;
        }

        if (!email.Contains("@") && !email.Contains("."))
        {
            return false;
        }

        if (age < 21)
        {
            return false;
        }
        
        return true;
    }
}