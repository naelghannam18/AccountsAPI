using AutoMapper;
using Domain.DTOs;
using Domain.Models;

namespace Infrastructure.Mappings;

public class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<Account, AccountDTO>().ReverseMap();
        CreateMap<Transaction, TransactionDTO>().ReverseMap();
        CreateMap<Customer, CustomerDTO>().ReverseMap();
    }
}
