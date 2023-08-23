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
    #region Private readonly fields
    private readonly IAccountsRepository AccountsRepository;
    private readonly IMapper Mapper;
    #endregion

    #region Constructor
    public GetAccountByIdQueryHandler(IAccountsRepository accountsRepository, IMapper mapper)
    {
        AccountsRepository = accountsRepository;
        Mapper = mapper;
    }
    #endregion

    #region Query Handler
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
    #endregion
}
