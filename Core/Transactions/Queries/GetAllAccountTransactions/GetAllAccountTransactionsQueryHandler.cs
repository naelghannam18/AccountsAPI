using System.Net;
using AutoMapper;
using Application.DTOs;
using Domain.Models;
using Domain.Contracts.Infrastructure;
using MediatR;

namespace Application.Transactions.Queries.GetAllAccountTransactions;

public class GetAllAccountTransactionsQueryHandler : IRequestHandler<GetAllAccountTransactionsQuery, Response<List<TransactionDTO>>>
{
    private readonly ITransactionsRepository TransactionsRepository;
    private readonly IMapper Mapper;

    public GetAllAccountTransactionsQueryHandler(ITransactionsRepository transactionsRepository, IMapper mapper)
    {
        TransactionsRepository = transactionsRepository;
        Mapper = mapper;
    }

    public async Task<Response<List<TransactionDTO>>> Handle(GetAllAccountTransactionsQuery request, CancellationToken cancellationToken)
    {
        var result = (await TransactionsRepository
                    .Get(t => t.SenderId.Equals(request.accountId) || t.ReceiverId.Equals(request.accountId)))
                    .Select(t => Mapper.Map<TransactionDTO>(t))
                    .ToList();

        return new()
        {
            Status = HttpStatusCode.OK,
            Data = result
        };
    }
}
