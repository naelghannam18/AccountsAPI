using Domain.DTOs;
using Domain.Models;

namespace Core.Services.Contracts
{
    public interface ITransactionService
    {
        Task<Response<string>> CreateTransaction(TransactionDTO request);
        Task DeleteTransaction(params string[] ids);
        Task<Response<List<TransactionDTO>>> GetAllTransactions(string accountId);
        Task<Response<TransactionDTO>> GetTransactionById(string id);
    }
}