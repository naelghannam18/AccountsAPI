using System.Net;
using AutoMapper;
using Core.Services.Contracts;
using Domain.DTOs;
using Domain.Models;
using Infrastructure.Repositories.Contracts;

namespace Core.Services.Implementations;

public class AccountService : IAccountService
{
    #region Private Read only Fields
    private readonly IAccountsRepository AccountsRepository;
    private readonly ITransactionsRepository TransactionsRepository;
    private readonly ICustomersRepository CustomersRepository;
    private readonly IMapper Mapper;

    public AccountService(IAccountsRepository accountsRepository, ITransactionsRepository transactionsRepository, IMapper mapper, ICustomersRepository customersRepository)
    {
        AccountsRepository = accountsRepository;
        TransactionsRepository = transactionsRepository;
        Mapper = mapper;
        CustomersRepository = customersRepository;
    }
    #endregion

    #region Constructor

    #endregion

    #region Get All Accounts
    public async Task<Response<List<AccountDTO>>> GetAllAccounts(string customerId)
    {
        var result = (await AccountsRepository.Get(a => a.CustomerId == customerId && !a.IsRemoved))
            .Select(a => Mapper.Map<AccountDTO>(a))
            .ToList();

        return new()
        {
            Status = HttpStatusCode.OK,
            Data = result
        };
    }
    #endregion

    #region Get Account By ID
    public async Task<Response<AccountDTO>> GetAccountById(string id)
    {
        var result = await AccountsRepository.GetById(id);
        if (result is not null)
        {
            return new()
            {
                Status = HttpStatusCode.OK,
                Data = Mapper.Map<AccountDTO>(result)
            };
        }
        else
        {
            return new()
            {
                Status = HttpStatusCode.NotFound
            };
        }

    }
    #endregion

    #region Create Account
    public async Task<Response<string>> CreateAccount(CreateAccountDTO request)
    {
        if (request is null) return new() { Status = HttpStatusCode.BadRequest, Message = "Please Provide Values" };

        var customer = await CustomersRepository.GetById(request.CustomerId);

        if (customer is null) { return new() { Status = HttpStatusCode.BadRequest, Message = "Customer DOes Not exist" }; }

        var entity = new Account()
        {
            Balance = 0 + request.InitialCredit,
            CustomerId = customer.Id,
            Customer = customer,
            SentTransactions = new(),
            ReceivedTransactions = new()
        };

        if (request.InitialCredit > 0)
        {
            var transaction = new Transaction()
            {
                Amount = request.InitialCredit,
            };
            entity.ReceivedTransactions.Add(transaction);
        }
        var result = await AccountsRepository.Create(entity);

        return new()
        {
            Status = HttpStatusCode.Created,
            Data = result.Id
        };


    }
    #endregion

    #region Delete Account
    public async Task DeleteAccounts(params string[] ids)
    {
        await AccountsRepository.Delete(ids: ids);
    }
    #endregion
}
