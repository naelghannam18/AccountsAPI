using System.Net;
using AutoMapper;
using Application.DTOs;
using Domain.Models;
using Domain.Contracts.Infrastructure;
using MediatR;

namespace Application.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, Response<List<CustomerDTO>>>
{
    #region Private Readonly Fields
    private readonly ICustomersRepository Repository;
    private readonly IMapper Mapper;
    #endregion

    #region Constructor
    public GetAllCustomersQueryHandler(ICustomersRepository repository, IMapper mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }
    #endregion

    #region Query Handler
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
    #endregion
}
