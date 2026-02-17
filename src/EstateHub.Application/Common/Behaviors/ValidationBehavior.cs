using FluentValidation;
using MediatR;
using ValidationException = EstateHub.Application.Common.Exceptions.ValidationException;

namespace EstateHub.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var errors = _validators
            .Select(v => v.Validate(context))
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .GroupBy(x => x.PropertyName, x => x.ErrorMessage)
            .ToDictionary(g => g.Key, g => g.ToArray());

        if (errors.Any())
            throw new ValidationException(errors);

        return await next();
    }
}