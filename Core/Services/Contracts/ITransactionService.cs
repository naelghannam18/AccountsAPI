using Domain.DTOs;
using Domain.Models;

namespace Core.Services.Contracts
{
    public interface ITransactionService
    {
        Task<Response<int>> CreateTransaction(TransactionDTO request);
        Task DeleteTransaction(params int[] ids);
        Task<Response<List<TransactionDTO>>> GetAllTransactions(int accountId);
        Task<Response<TransactionDTO>> GetTransactionById(int id);
    }
}