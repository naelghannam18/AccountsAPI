using AutoMapper;
using Core.Services.Implementations;
using Domain.DTOs;
using Domain.Models;
using FluentAssertions;
using Infrastructure.Repositories;
using Moq;
using System.Net;
using Xunit;

namespace Core.Tests;

/// <summary>
/// This is a sample Unit test for Customer Service
/// I Have little knowledge of Unit Tests 
/// I did a brief research in respect to the project deadline
/// This class showcases my learning outcomes of Unit testing in .Net
/// </summary>
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

        Mapper.Setup(m => m.Map<CustomerDTO>(It.IsAny<Customer>()))
            .Returns((Customer c) => new CustomerDTO
            { Id = c.Id, Name = c.Name, Surname = c.Surname });

        // Act
        var response = await CustomerService.GetAllCustomers();

        // Assert

        response.Status.Should().Be(HttpStatusCode.OK);
        customerDTOs.Count.Should().Be(response.Data.Count);
        response.Data.Should().BeOfType(typeof(List<CustomerDTO>));

        for (int i = 0; i < customerDTOs.Count; i++)
        {
            customerDTOs[i].Should().BeEquivalentTo(response.Data[i]);
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

        var customerDTO = new CustomerDTO()
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
        response.Status.Should().Be(HttpStatusCode.OK);
        response.Data.Should().BeEquivalentTo(customerDTO);
    }

    [Fact]
    public async Task CreateCustomer_ShouldReturnId()
    {
        var createCustomerDto = new CreateCustomerDTO()
        {
            Name = "Name",
            Surname = "Surname"
        };
        // Arrange
        var customer = new Customer()
        {
            Id = 1,
            Name = "Name",
            Surname = "Surname",
            Account = new()
            {
                Balance = 100,
                CreatedDate = DateTime.UtcNow,
                CustomerId = 1,
                Id = 1,
                IsRemoved = false,
                ReceivedTransactions = new List<Transaction>(),
                SentTransactions = new List<Transaction>(),
            }
        };
        Repository.Setup(r => r.Create(It.IsAny<Customer>())).ReturnsAsync(customer);

        // Act
        var response = await CustomerService.CreateCustomer(createCustomerDto);

        // Assert
        response.Status.Should().Be(HttpStatusCode.Created);
        response.Data.Should().BeGreaterThan(0);
    }
}