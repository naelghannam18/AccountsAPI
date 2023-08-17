using Infrastructure.Repositories.Contracts;
using MediatR;

namespace Application.Accounts.Commands.DeleteAccount;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
{
    private readonly IAccountsRepository AccountsRepository;

    public DeleteAccountCommandHandler(IAccountsRepository accountsRepository)
    {
        AccountsRepository = accountsRepository;
    }

    public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        await AccountsRepository.Delete(ids: request.ids);
    }
}
