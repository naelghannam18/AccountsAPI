using System.Net;
using AutoMapper;
using Domain.DTOs;
using Domain.Exceptions;
using Domain.Models;
using Infrastructure.Repositories.Contracts;
using MediatR;

namespace Application.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Response<CustomerDTO>>
{
    private readonly ICustomersRepository Repository;
    private readonly IMapper Mapper;

    public GetCustomerByIdQueryHandler(ICustomersRepository repository, IMapper mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }

    public async Task<Response<CustomerDTO>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var result = Mapper.Map<CustomerDTO>(await Repository.GetById(request.id));

        if (result is not null)
        {
            return new()
            {
                Status = HttpStatusCode.OK,
                Data = result
            };
        }
        else throw new CustomerDoesNotExistException(request.id);
    }
}
