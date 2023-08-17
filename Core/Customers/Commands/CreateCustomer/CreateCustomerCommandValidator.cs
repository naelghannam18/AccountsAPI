using FluentValidation;

namespace Application.Customers.Commands.CreateCustomer;

public sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.request.Name).MinimumLength(3);
        RuleFor(x => x.request.Surname).MinimumLength(3);
    }
}
