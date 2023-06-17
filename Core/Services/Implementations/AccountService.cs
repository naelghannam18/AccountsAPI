using AutoMapper;
using Core.Services.Contracts;
using Domain.DTOs;
using Domain.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core.Services.Implementations;

public class AccountService : IAccountService
{
    #region Private Read only Fields
    private readonly IGenericRepository<Account> Repository;
    private readonly IGenericRepository<Transaction> TransactionRepository;
    private readonly IMapper Mapper;
    #endregion

    #region Constructor
    public AccountService(IGenericRepository<Account> repo, IGenericRepository<Transaction> transactionRepo, IMapper mapper)
    {
        Repository = repo;
        TransactionRepository = transactionRepo;
        Mapper = mapper;
    }
    #endregion

    #region Get All Accounts
    public async Task<Response<List<AccountDTO>>> GetAllAccounts(int customerId)
    {
        var result = (await Repository.GetAll())
          .Include(a => a.ReceivedTransactions)
          .Include(a => a.SentTransactions)
          .Where(a => a.CustomerId == customerId)
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
    public async Task<Response<AccountDTO>> GetAccountById(int id)
    {
        var result = (await Repository.GetById(id)).Include(a => a.SentTransactions).Include(a => a.ReceivedTransactions).FirstOrDefault();
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
    public async Task<Response<int>> CreateAccount(CreateAccountDTO request)
    {
        if (request is null) return new() { Status = HttpStatusCode.BadRequest, Message = "Please Provide Values" };


        var entity = new Account()
        {
            Balance = 0 + request.InitialCredit,
            CustomerId = request.CustomerId,
        };
        var result = await Repository.Create(entity);

        if (request.InitialCredit > 0)
        {
            var transaction = new Transaction()
            {
                Amount = request.InitialCredit,
                SenderId = result.Id,
                ReceiverId = result.Id,
            };
            await TransactionRepository.Create(transaction);
        }

        return new()
        {
            Status = HttpStatusCode.Created,
            Data = result.Id
        };


    }
    #endregion

    #region Delete Account
    public async Task DeleteAccounts(params int[] ids)
    {
        await Repository.Delete(ids: ids);
    }
    #endregion
}
