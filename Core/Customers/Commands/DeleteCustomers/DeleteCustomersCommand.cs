
using MediatR;

namespace Application.Customers.Commands.DeleteCustomers;

public sealed record DeleteCustomersCommand(params string[] ids) : IRequest
{
}
