using FluentValidation;
using MediatR;

namespace Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> Validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        Validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!Validators.Any())
        {
            await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationErrors = Validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .ToList();

        if (validationErrors.Any())
        {
            throw new FluentValidation.ValidationException(validationErrors);
        }

        return await next();
    }
}
