using Domain.DTOs;
using Domain.Models;
using MediatR;

namespace Application.Accounts.Queries.GetAllAccounts;

public sealed record GetAllAccountsQuery(string customerId) : IRequest<Response<List<AccountDTO>>>
{
}
