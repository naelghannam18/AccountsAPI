using System.Net;
using AutoMapper;
using Application.DTOs;
using Domain.Exceptions;
using Domain.Models;
using Domain.Contracts.Infrastructure;
using MediatR;

namespace Application.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Response<CustomerDTO>>
{
    #region Private Readonly Fields
    private readonly ICustomersRepository Repository;
    private readonly IMapper Mapper;
    #endregion

    #region Constructor
    public GetCustomerByIdQueryHandler(ICustomersRepository repository, IMapper mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }
    #endregion

    #region Query Handler
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
    #endregion
}
