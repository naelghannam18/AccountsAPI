using Application.DTOs;
using Domain.Models;
using MediatR;

namespace Application.Customers.Queries.GetCustomerById;

public sealed record GetCustomerByIdQuery(string id) : IRequest<Response<CustomerDTO>>
{
}
