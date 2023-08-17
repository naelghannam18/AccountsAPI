using FluentValidation;

namespace Application.Customers.Commands.DeleteCustomers;

public sealed class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomersCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(x => x.ids).NotEmpty().NotNull();
    }
}
