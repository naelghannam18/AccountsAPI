using System.Net;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models;
using Domain.Contracts.Infrastructure;
using MediatR;

namespace Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Response<string>>
{
    private readonly ICustomersRepository Repository;

    public CreateCustomerCommandHandler(ICustomersRepository repository)
    {
        Repository = repository;
    }

    public async Task<Response<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ModelValidationException(new List<string>() { "Please Provide Missing Values" });

        var result = await Repository.Create(new Customer { Name = request.request.Name, Surname = request.request.Surname });

        return new()
        {
            Status = HttpStatusCode.Created,
            Data = result.Id
        };
    }
}
