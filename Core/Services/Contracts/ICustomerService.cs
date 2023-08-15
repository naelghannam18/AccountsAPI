using Domain.DTOs;
using Domain.Models;

namespace Core.Services.Contracts
{
    public interface ICustomerService
    {
        Task<Response<string>> CreateCustomer(CreateCustomerDTO request);
        Task DeleteCustomers(params string[] ids);
        Task<Response<List<CustomerDTO>>> GetAllCustomers();
        Task<Response<CustomerDTO>> GetCustomerById(string id);
        Task<Response<bool>> UpdateCustomer(UpdateCustomerDTO request);
    }
}