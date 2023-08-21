using Application.DTOs;
using Domain.Models;
using MediatR;

namespace Application.Customers.Queries.GetAllCustomers;

public sealed record GetAllCustomersQuery() : IRequest<Response<List<CustomerDTO>>>
{
}
