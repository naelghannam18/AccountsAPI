using AutoMapper;
using Application.DTOs;
using Domain.Models;

namespace Application.Mappings;

public class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<Account, AccountDTO>().ReverseMap();
        CreateMap<Transaction, TransactionDTO>().ReverseMap();
        CreateMap<Customer, CustomerDTO>().ReverseMap();
    }
}
