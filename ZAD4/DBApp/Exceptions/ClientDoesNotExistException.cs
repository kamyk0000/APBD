namespace DBApp.Exceptions;

public class ClientDoesNotExistException : ClientException
{
    private const string DefaultMessage = "This Client does not exist; Process terminated";

    public ClientDoesNotExistException() : base(DefaultMessage)
    {
    }

    public ClientDoesNotExistException(string message)
        : base(message)
    {
    }

    public ClientDoesNotExistException(string message, Exception inner)
        : base(message, inner)
    {
    }
}