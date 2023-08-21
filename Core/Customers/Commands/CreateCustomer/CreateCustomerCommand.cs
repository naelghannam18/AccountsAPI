using Application.DTOs;
using Domain.Models;
using MediatR;

namespace Application.Customers.Commands.CreateCustomer;

public sealed record CreateCustomerCommand(CreateCustomerDTO request) : IRequest<Response<string>>
{
}
