using Application.DTOs;
using Domain.Models;
using MediatR;

namespace Application.Transactions.Commands.CreateTransaction;

public sealed record CreateTransactionCommand(TransactionDTO request) : IRequest<Response<string>>
{
}
