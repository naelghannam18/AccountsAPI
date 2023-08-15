#region Usings
using AutoMapper;
using Core.Services.Contracts;
using Domain.DTOs;
using Domain.Exceptions;
using Domain.Models;
using Infrastructure.Repositories.Contracts;
using System.Net;
#endregion

namespace Core.Services.Implementations;

public class CustomerService : ICustomerService
{
    #region Private Read only Fields
    private readonly ICustomersRepository Repository;
    private readonly IMapper Mapper;
    #endregion

    #region Constructor
    public CustomerService(ICustomersRepository repo, IMapper mapper)
    {
        Repository = repo;
        Mapper = mapper;
    }

    #endregion

    #region Get All Customers
    public async Task<Response<List<CustomerDTO>>> GetAllCustomers()
    {
        var result = (await Repository.GetAll())
          .Select(c => Mapper.Map<CustomerDTO>(c))
          .ToList();

        return new()
        {
            Status = HttpStatusCode.OK,
            Data = result
        };
    }
    #endregion

    #region Get Customer By ID
    public async Task<Response<CustomerDTO>> GetCustomerById(string id)
    {
        var result = Mapper.Map<CustomerDTO>(await Repository.GetById(id));

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

    #region Create Customer
    public async Task<Response<string>> CreateCustomer(CreateCustomerDTO request)
    {
        if (request is null) return new() { Status = HttpStatusCode.BadRequest, Message = "Please Provide Values" };

        var result = await Repository.Create(new Customer { Name = request.Name, Surname = request.Surname });

        return new()
        {
            Status = HttpStatusCode.Created,
            Data = result.Id
        };

    }
    #endregion

    #region Delete Customers
    public async Task DeleteCustomers(params string[] ids)
    {
        await Repository.Delete(ids: ids);
    }
    #endregion

    #region Update Customer
    public async Task<Response<bool>> UpdateCustomer(UpdateCustomerDTO request)
    {
        if (request is null) return new() { Status = HttpStatusCode.BadRequest, Message = "Please Provide Value" };

        var customer = await Repository.GetById(request.Id);
        if (customer is not null)
        {
            customer.Name = request.Name;
            customer.Surname = request.Surname;

            await Repository.Update(customer);
            return new()
            {
                Status = HttpStatusCode.NoContent
            };
        }
        else
        {
            throw new CustomerDoesNotExistError("Customer Does Not Exist");
        }
    }
    #endregion
}
