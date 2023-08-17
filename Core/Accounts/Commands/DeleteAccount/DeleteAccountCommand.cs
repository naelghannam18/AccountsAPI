using MediatR;

namespace Application.Accounts.Commands.DeleteAccount;

public sealed record DeleteAccountCommand(params string[] ids) : IRequest
{
}
