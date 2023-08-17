using FluentValidation;

namespace Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(x => x.request.Name).NotNull().NotEmpty().MinimumLength(3);
        RuleFor(x => x.request.Surname).NotNull().NotEmpty().MinimumLength(3);

    }
}
