using AutoMapper;
using Core.Services.Implementations;
using Domain.DTOs;
using Domain.Models;
using Infrastructure.Repositories;
using Moq;
using System.Net;
using Xunit;

namespace Core.Tests;

public class CustomerServiceTests
{
    private readonly Mock<IGenericRepository<Customer>> Repository;
    private readonly Mock<IMapper> Mapper;
    private readonly CustomerService CustomerService;

    public CustomerServiceTests()
    {
        Repository = new Mock<IGenericRepository<Customer>>();
        Mapper = new Mock<IMapper>();
        CustomerService = new CustomerService(Repository.Object, Mapper.Object);
    }

    [Fact]
    public async Task GetAllCustomers_ReturnsAllCustomers()
    {
        // Arrange
        var customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "John", Surname = "Doe" },
            new Customer { Id = 2, Name = "Jane", Surname = "Smith" }
        };
        var customerDTOs = customers.Select(c => new CustomerDTO { Id = c.Id, Name = c.Name, Surname = c.Surname }).ToList();
        Repository.Setup(r => r.GetAll()).ReturnsAsync(customers.AsQueryable());
        Mapper.Setup(m => m.Map<CustomerDTO>(It.IsAny<Customer>())).Returns((Customer c) =>
            new CustomerDTO { Id = c.Id, Name = c.Name, Surname = c.Surname });

        // Act
        var response = await CustomerService.GetAllCustomers();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.Status);
        Assert.True(customerDTOs.Count == response.Data.Count);

        for (int i = 0; i < customerDTOs.Count; i++)
        {
            Assert.Equal(customerDTOs[i].Id, response.Data[i].Id);
            Assert.Equal(customerDTOs[i].Name, response.Data[i].Name);
            Assert.Equal(customerDTOs[i].Surname, response.Data[i].Surname);
            Assert.Equal(customerDTOs[i].Account, response.Data[i].Account);
        }
    }


    [Fact]
    public async Task GetCustomerById_ReturnsCorrectCustomer()
    {
        // Arrange
        var customer = new List<Customer>()
        {
            new Customer { Id = 1, Name = "John", Surname = "Doe" }
        };

        var customerDTOs = new CustomerDTO()
        {
            Name = customer[0].Name,
            Surname = customer[0].Surname,
            Id = customer[0].Id,
        };
        Repository.Setup(r => r.GetById(1)).ReturnsAsync(customer.AsQueryable());
        Mapper.Setup(m => m.Map<CustomerDTO>(It.IsAny<Customer>()))
            .Returns((Customer c) => new CustomerDTO { Id = c.Id, Name = c.Name, Surname = c.Surname });

        // Act
        var response = await CustomerService.GetCustomerById(1);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.Status);
        Assert.Equal(customer[0].Id, response.Data.Id);
        Assert.Equal(customer[0].Name, response.Data.Name);
        Assert.Equal(customer[0].Surname, response.Data.Surname);
    }

    [Fact]
    public async Task CreateCustomer_ShouldReturnId()
    {
        // Arrange
        var customer = new Customer()
        {
            Id = 1,
            Name = "Name",
            Surname = "Surname"
        };

        Repository.Setup()

        // Act

        // Assert
    }
}