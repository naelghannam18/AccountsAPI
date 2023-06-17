using Domain.DTOs;
using Domain.Models;

namespace Core.Services.Contracts
{
    public interface IAccountService
    {
        Task<Response<int>> CreateAccount(CreateAccountDTO request);
        Task DeleteAccounts(params int[] ids);
        Task<Response<AccountDTO>> GetAccountById(int id);
        Task<Response<List<AccountDTO>>> GetAllAccounts(int customerId);
    }
}