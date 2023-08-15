using Domain.DTOs;
using Domain.Models;

namespace Core.Services.Contracts
{
    public interface IAccountService
    {
        Task<Response<string>> CreateAccount(CreateAccountDTO request);
        Task DeleteAccounts(params string[] ids);
        Task<Response<AccountDTO>> GetAccountById(string id);
        Task<Response<List<AccountDTO>>> GetAllAccounts(string customerId);
    }
}