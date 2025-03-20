namespace Tutorial3.Containers;

public class OverfillException : Exception
{
    public OverfillException(string message) : base(message) { Console.WriteLine(message); }
    
}