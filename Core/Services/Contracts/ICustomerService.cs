using Domain.DTOs;
using Domain.Models;

namespace Core.Services.Contracts
{
    public interface ICustomerService
    {
        Task<Response<int>> CreateCustomer(CreateCustomerDTO request);
        Task DeleteCustomers(params int[] ids);
        Task<Response<List<CustomerDTO>>> GetAllCustomers();
        Task<Response<CustomerDTO>> GetCustomerById(int id);
        Task<Response<bool>> UpdateCustomer(UpdateCustomerDTO request);
    }
}