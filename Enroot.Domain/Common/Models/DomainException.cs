using ErrorOr;

namespace Enroot.Domain.Common.Models;

public class DomainException : Exception
{
    public Error Error { get; }

    public DomainException(Error error) : base($"{error.Code}.{error.Description}")
    {
        Error = error;
    }

    private DomainException() : base()
    {
    }
}