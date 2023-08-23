using Domain.Exceptions;
using Domain.Contracts.Infrastructure;
using MediatR;

namespace Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    #region Private Readonly Fields
    private readonly ICustomersRepository CustomerRepository;
    #endregion

    #region Constructor
    public UpdateCustomerCommandHandler(ICustomersRepository repository)
    {
        CustomerRepository = repository;
    }
    #endregion

    #region Command Handler
    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ModelValidationException(new() { "Please Provide Missing Values" });

        var customer = await CustomerRepository.GetById(request.request.Id);
        if (customer is not null)
        {
            customer.Name = request.request.Name;
            customer.Surname = request.request.Surname;

            await CustomerRepository.Update(customer);
        }
        else
        {
            throw new CustomerDoesNotExistException("Customer Does Not Exist");
        }
    } 
    #endregion
}
