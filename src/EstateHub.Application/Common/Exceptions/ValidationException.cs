namespace EstateHub.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(IDictionary<string, string[]> errors)
        : base("Validation xətası baş verdi.")
    {
        Errors = errors;
    }
}