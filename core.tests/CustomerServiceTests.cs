using AutoMapper;
using Core.Services.Implementations;
using Domain.DTOs;
using Domain.Models;
using Infrastructure.Repositories;
using Moq;
using System.Net;
using Xunit;

namespace core.tests;

public class CustomerServiceTests
{
    private readonly Mock<IGenericRepository<Customer>> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CustomerService _customerService;

    public CustomerServiceTests()
    {
        _repositoryMock = new Mock<IGenericRepository<Customer>>();
        _mapperMock = new Mock<IMapper>();
        _customerService = new CustomerService(_repositoryMock.Object, _mapperMock.Object);
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
        _repositoryMock.Setup(r => r.GetAll().Result.ToList()).Returns(customers);
        _mapperMock.Setup(m => m.Map<CustomerDTO>(It.IsAny<Customer>())).Returns((Customer c) =>
            new CustomerDTO { Id = c.Id, Name = c.Name, Surname = c.Surname });

        // Act
        var response = await _customerService.GetAllCustomers();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.Status);
        Assert.Equal(customerDTOs, response.Data);
    }
}
