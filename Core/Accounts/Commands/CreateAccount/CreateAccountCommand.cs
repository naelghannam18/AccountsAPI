using Application.DTOs;
using Domain.Models;
using MediatR;

namespace Application.Accounts.Commands.CreateAccount;

public sealed record CreateAccountCommand(CreateAccountDTO request) : IRequest<Response<string>>
{
}
