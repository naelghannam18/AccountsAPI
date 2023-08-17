using Domain.DTOs;
using Domain.Models;
using MediatR;

namespace Application.Accounts.Queries.GetAccountById;

public sealed record GetAccountByIdQuery(string accountId) : IRequest<Response<AccountDTO>>
{
}
