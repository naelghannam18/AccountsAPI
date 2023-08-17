using System.Net;
using AutoMapper;
using Domain.DTOs;
using Domain.Exceptionsl;
using Domain.Models;
using Infrastructure.Repositories.Contracts;
using MediatR;

namespace Application.Transactions.Queries.GetTransactionById;

public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, Response<TransactionDTO>>
{
    private readonly ITransactionsRepository TransactionsRepository;
    private readonly IMapper Mapper;

    public GetTransactionByIdQueryHandler(ITransactionsRepository transactionRepository, IMapper mapper)
    {
        TransactionsRepository = transactionRepository;
        Mapper = mapper;
    }

    public async Task<Response<TransactionDTO>> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var result = Mapper.Map<TransactionDTO>(await TransactionsRepository.GetById(request.transactionId));

        if (result is not null)
        {
            return new()
            {
                Status = HttpStatusCode.OK,
                Data = result
            };
        }
        else throw new TransactionNotFoundException(request.transactionId);
    }
}
