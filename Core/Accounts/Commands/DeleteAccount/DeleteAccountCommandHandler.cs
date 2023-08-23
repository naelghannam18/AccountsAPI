using Domain.Contracts.Infrastructure;
using MediatR;

namespace Application.Accounts.Commands.DeleteAccount;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
{
    #region Private Readonly Fields
    private readonly IAccountsRepository AccountsRepository;
    #endregion

    #region Constructor
    public DeleteAccountCommandHandler(IAccountsRepository accountsRepository)
    {
        AccountsRepository = accountsRepository;
    }
    #endregion

    #region Command Handler
    public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        await AccountsRepository.Delete(ids: request.ids);
    } 
    #endregion
}
