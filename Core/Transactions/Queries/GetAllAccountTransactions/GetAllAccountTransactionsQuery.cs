using Domain.DTOs;
using Domain.Models;
using MediatR;

namespace Application.Transactions.Queries.GetAllAccountTransactions;

public sealed record GetAllAccountTransactionsQuery(string accountId) : IRequest<Response<List<TransactionDTO>>>
{
}
