using Domain.DTOs;
using MediatR;

namespace Application.Customers.Commands.UpdateCustomer;

public sealed record UpdateCustomerCommand(UpdateCustomerDTO request) : IRequest
{
}
