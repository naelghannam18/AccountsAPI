#region Usings
using Core.Services.Contracts;
using Domain.DTOs;
using Domain.Exceptions;
using Domain.Models;
using Infrastructure.Repositories.Contracts;
using System.Net;

#endregion
namespace Core.Services.Implementations;

public class TransactionService : ITransactionService
{
    #region Private Read only Fields
    private readonly ITransactionsRepository TransactionsRepository;
    private readonly IAccountsRepository AccountRepository;

    public TransactionService(ITransactionsRepository transactionsRepository, IAccountsRepository accountRepository)
    {
        TransactionsRepository = transactionsRepository;
        AccountRepository = accountRepository;
    }
    #endregion

    #region Constructor

    #endregion

    #region Get All Transactions
    public async Task<Response<List<TransactionDTO>>> GetAllTransactions(string accountId)
    {
        var result =
            (await TransactionsRepository
            .Get(t => t.SenderId.Equals(accountId) || t.ReceiverId.Equals(accountId)))
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
    public async Task<Response<TransactionDTO>> GetTransactionById(string id)
    {
        var result = Selector().Invoke(await TransactionsRepository.GetById(id));

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
    public async Task<Response<string>> CreateTransaction(TransactionDTO request)
    {
        if (request is null) return new() { Status = HttpStatusCode.BadRequest, Message = "Please Provide Values" };

        var sender = await AccountRepository.GetById(request.SenderId);
        var receiver = await AccountRepository.GetById(request.ReceiverId);

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
        var result = await TransactionsRepository.Create(entity);

        return new()
        {
            Status = HttpStatusCode.Created,
            Data = result.Id
        };
    }
    #endregion

    #region Delete Transaction
    public async Task DeleteTransaction(params string[] ids)
    {
        await TransactionsRepository.Delete(ids: ids);
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
            SenderId = entity.SenderId
        };
    }

}
