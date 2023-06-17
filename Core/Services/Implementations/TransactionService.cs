#region Usings
using Core.Services.Contracts;
using Domain.DTOs;
using Domain.Exceptions;
using Domain.Models;
using Infrastructure.Repositories;
using System.Net;

#endregion
namespace Core.Services.Implementations;

public class TransactionService : ITransactionService
{
    #region Private Read only Fields
    private readonly IGenericRepository<Transaction> Repository;
    private readonly IGenericRepository<Account> AccountRepository;
    #endregion

    #region Constructor
    public TransactionService(IGenericRepository<Transaction> repo, IGenericRepository<Account> accountRepo)
    {
        Repository = repo;
        AccountRepository = accountRepo;
    }
    #endregion

    #region Get All Transactions
    public async Task<Response<List<TransactionDTO>>> GetAllTransactions(int accountId)
    {
        var queryableResult = (await Repository.GetAll());

        var result = queryableResult
          .Where(t => t.SenderId == accountId || t.ReceiverId == accountId)
          .Select(Selector())
          .ToList();

        return new()
        {
            Status = HttpStatusCode.OK,
            Data = result
        };
    }
    #endregion

    #region Get Transaction By ID
    public async Task<Response<TransactionDTO>> GetTransactionById(int id)
    {
        var result = (await Repository.GetById(id))
            .Select(Selector())
            .FirstOrDefault();

        if (result is not null)
        {
            return new()
            {
                Status = HttpStatusCode.OK,
                Data = result
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

    #region Create Transaction
    public async Task<Response<int>> CreateTransaction(TransactionDTO request)
    {
        if (request is null) return new() { Status = HttpStatusCode.BadRequest, Message = "Please Provide Values" };

        var sender = (await AccountRepository.GetById(request.SenderId)).FirstOrDefault();
        var receiver = (await AccountRepository.GetById(request.ReceiverId)).FirstOrDefault();

        if (sender is null || receiver is null)
        {
            throw new AccountDoesNotExistError("Account Does Not Exist");
        }

        if (sender.Balance < request.Amount)
        {
            throw new AccountInsufficientFundsError("Insufficient Funds");
        }

        var entity = new Transaction()
        {
            Amount = request.Amount,
            SenderId = request.SenderId,
            ReceiverId = request.ReceiverId,
        };
        var result = await Repository.Create(entity);

        sender.Balance -= entity.Amount;
        receiver.Balance += entity.Amount;

        await AccountRepository.Update(sender);
        await AccountRepository.Update(receiver);

        return new()
        {
            Status = HttpStatusCode.Created,
            Data = result.Id
        };
    }
    #endregion

    #region Delete Transaction
    public async Task DeleteTransaction(params int[] ids)
    {
        await Repository.Delete(ids: ids);
    }
    #endregion

    private static Func<Transaction, TransactionDTO> Selector()
    {
        return entity => entity == null ? null :
        new TransactionDTO()
        {
            Amount = entity.Amount,
            CreatedDate = entity.CreatedDate,
            Id = entity.Id,
            ReceiverId = entity.ReceiverId,
            SenderId = entity.ReceiverId
        };
    }

}
