namespace DBApp.Exceptions;

public class ClientHasTripsException : ClientException
{
    private const string DefaultMessage = "This Client has Trips connected to them; Process terminated";

    public ClientHasTripsException() : base(DefaultMessage)
    {
    }

    public ClientHasTripsException(string message)
        : base(message)
    {
    }

    public ClientHasTripsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}