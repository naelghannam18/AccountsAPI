#region Usings
using Application.Accounts.Commands.CreateAccount;
using Application.Accounts.Commands.DeleteAccount;
using Application.Accounts.Queries.GetAccountById;
using Application.Accounts.Queries.GetAllAccounts;
using Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
#endregion

namespace AccountsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Private Read only Fields
        private readonly ISender Sender;
        #endregion

        #region Constructor
        public AccountController(ISender sender)
        {
            Sender = sender;
        }
        #endregion

        #region Web Actions
        #region Get All Customer Accounts
        [HttpGet]
        [Route("customer/{customerId}")]
        public async Task<ActionResult<List<AccountDTO>>> GetAllAccounts(string customerId, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(new GetAllAccountsQuery(customerId), cancellationToken);
            return Ok(result.Data);
        }
        #endregion

        #region Get Account By Id
        [HttpGet]
        [Route("{accountId}", Name = "GetAccountById")]
        public async Task<ActionResult<AccountDTO>> GetAccountById(string accountId, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(new GetAccountByIdQuery(accountId), cancellationToken);
            return Ok(result.Data);
        }
        #endregion

        #region Create Account
        [HttpPost]
        public async Task<ActionResult> CreateAccount([FromBody] CreateAccountDTO request, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(new CreateAccountCommand(request), cancellationToken);
            return CreatedAtRoute("GetAccountById", new { accountId = result.Data }, result.Data);
        }
        #endregion

        #region Delete Accounts

        [HttpDelete]
        public async Task<ActionResult> DeleteAccounts(string[] ids, CancellationToken cancellationToken)
        {
            await Sender.Send(new DeleteAccountCommand(ids), cancellationToken);
            return NoContent();
        }
        #endregion 
        #endregion
    }
}
