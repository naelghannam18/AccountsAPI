using Domain.Contracts.Infrastructure;
using MediatR;

namespace Application.Customers.Commands.DeleteCustomers;

public class DeleteCustomersCommandHandler : IRequestHandler<DeleteCustomersCommand>
{
    #region Private Readonly Fields
    private readonly ICustomersRepository Repository;
    #endregion

    #region Constructor
    public DeleteCustomersCommandHandler(ICustomersRepository repository)
    {
        Repository = repository;
    }
    #endregion

    #region Command Handler
    public async Task Handle(DeleteCustomersCommand request, CancellationToken cancellationToken)
    {
        await Repository.Delete(ids: request.ids);
    }
    #endregion
}
