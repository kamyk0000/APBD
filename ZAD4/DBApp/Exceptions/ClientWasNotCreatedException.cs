namespace DBApp.Exceptions;

public class ClientWasNotCreatedException : ClientException
{
    private const string DefaultMessage = "Client was not created; Process terminated";

    public ClientWasNotCreatedException() : base(DefaultMessage)
    {
    }

    public ClientWasNotCreatedException(string message)
        : base(message)
    {
    }

    public ClientWasNotCreatedException(string message, Exception inner)
        : base(message, inner)
    {
    }
}