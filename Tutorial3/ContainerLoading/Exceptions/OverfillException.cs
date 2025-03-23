namespace Tutorial3.Containers;

public class OverfillException(string message = "Maximum payload is exceeded") : Exception(message);