using AutoMapper;
using System.Net;
using Application.DTOs;
using Domain.Models;
using MediatR;
using Domain.Contracts.Infrastructure;

namespace Application.Accounts.Queries.GetAllAccounts;

public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, Response<List<AccountDTO>>>
{
    #region Private Readonly Fields
    private readonly IAccountsRepository AccountsRepository;
    private readonly IMapper Mapper;
    #endregion

    #region Constructor
    public GetAllAccountsQueryHandler(IAccountsRepository accountsRepository, IMapper mapper)
    {
        AccountsRepository = accountsRepository;
        Mapper = mapper;
    }
    #endregion

    #region Query Handler
    public async Task<Response<List<AccountDTO>>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        var result = (await AccountsRepository.Get(a => a.CustomerId == request.customerId && !a.IsRemoved))
            .Select(a => Mapper.Map<AccountDTO>(a))
            .ToList();

        return new()
        {
            Status = HttpStatusCode.OK,
            Data = result
        };
    } 
    #endregion
}
