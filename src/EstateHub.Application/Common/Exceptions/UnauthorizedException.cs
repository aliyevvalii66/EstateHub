namespace EstateHub.Application.Common.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message = "Bu əməliyyat üçün icazəniz yoxdur.")
        : base(message) { }
}