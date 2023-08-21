using Application.DTOs;
using Domain.Models;
using MediatR;

namespace Application.Transactions.Queries.GetTransactionById;

public record GetTransactionByIdQuery(string transactionId) : IRequest<Response<TransactionDTO>> 
{
}
