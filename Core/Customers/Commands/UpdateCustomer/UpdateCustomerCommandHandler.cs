using Domain.Exceptions;
using Infrastructure.Repositories.Contracts;
using MediatR;

namespace Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly ICustomersRepository Repository;

    public UpdateCustomerCommandHandler(ICustomersRepository repository)
    {
        Repository = repository;
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ModelValidationException(new() { "Please Provide Missing Values" });

        var customer = await Repository.GetById(request.request.Id);
        if (customer is not null)
        {
            customer.Name = request.request.Name;
            customer.Surname = request.request.Surname;

            await Repository.Update(customer);
        }
        else
        {
            throw new CustomerDoesNotExistException("Customer Does Not Exist");
        }
    }
}
