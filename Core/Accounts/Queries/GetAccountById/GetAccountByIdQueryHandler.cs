using System.Net;
using AutoMapper;
using Application.DTOs;
using Domain.Models;
using Domain.Contracts.Infrastructure;
using MediatR;
using Domain.Exceptions;

namespace Application.Accounts.Queries.GetAccountById;

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, Response<AccountDTO>>
{
    private readonly IAccountsRepository AccountsRepository;
    private readonly IMapper Mapper;

    public GetAccountByIdQueryHandler(IAccountsRepository accountsRepository, IMapper mapper)
    {
        AccountsRepository = accountsRepository;
        Mapper = mapper;
    }

    public async Task<Response<AccountDTO>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await AccountsRepository.GetById(request.accountId);
        if (result is not null)
        {
            return new()
            {
                Status = HttpStatusCode.OK,
                Data = Mapper.Map<AccountDTO>(result)
            };
        }
        else throw new AccountDoesNotExistException(request.accountId);
    }
}
