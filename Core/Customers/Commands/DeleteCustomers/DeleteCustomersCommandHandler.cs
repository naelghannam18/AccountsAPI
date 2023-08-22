using AutoMapper;
using Domain.Contracts.Infrastructure;
using MediatR;

namespace Application.Customers.Commands.DeleteCustomers;

public class DeleteCustomersCommandHandler : IRequestHandler<DeleteCustomersCommand>
{
    private readonly ICustomersRepository Repository;

    public DeleteCustomersCommandHandler(ICustomersRepository repository)
    {
        Repository = repository;
    }

    public async Task Handle(DeleteCustomersCommand request, CancellationToken cancellationToken)
    {
        await Repository.Delete(ids: request.ids);
    }
}
