using System.Net;
using AutoMapper;
using Application.DTOs;
using Domain.Models;
using Infrastructure.Repositories.Contracts;
using MediatR;

namespace Application.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, Response<List<CustomerDTO>>>
{
    private readonly ICustomersRepository Repository;
    private readonly IMapper Mapper;

    public GetAllCustomersQueryHandler(ICustomersRepository repository, IMapper mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }

    public async Task<Response<List<CustomerDTO>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
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
}
