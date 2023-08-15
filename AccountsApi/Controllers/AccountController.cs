#region Usings
using Core.Services.Contracts;
using Domain.DTOs;
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
        private readonly IAccountService AccountService;
        #endregion

        #region Constructor
        public AccountController(IAccountService service)
        {
            AccountService = service;
        }
        #endregion

        #region Web Actions
        #region Get All Customer Accounts
        [HttpGet]
        [Route("customer/{customerId}")]
        public async Task<ActionResult<List<AccountDTO>>> GetAllAccounts(string customerId)
        {
            var result = await AccountService.GetAllAccounts(customerId);
            return Ok(result.Data);
        }
        #endregion

        #region Get Account By Id
        [HttpGet]
        [Route("{accountId}", Name = "GetAccountById")]
        public async Task<ActionResult<AccountDTO>> GetAccountById(string accountId)
        {
            var result = await AccountService.GetAccountById(accountId);
            return result.Status switch
            {
                HttpStatusCode.OK => Ok(result.Data),
                HttpStatusCode.NotFound => NotFound(),
                _ => StatusCode(500, result.Message)
            };
        }
        #endregion

        #region Create Account
        [HttpPost]
        public async Task<ActionResult> CreateAccount([FromBody] CreateAccountDTO request)
        {
            var result = await AccountService.CreateAccount(request);
            return result.Status switch
            {
                HttpStatusCode.Created => CreatedAtRoute("GetAccountById", new { accountId = result.Data }, result.Data),
                _ => StatusCode(500, result.Message)
            };
        }
        #endregion

        #region Delete Accounts

        [HttpDelete]
        public async Task<ActionResult> DeleteAccounts(string[] ids)
        {
            await AccountService.DeleteAccounts(ids);
            return NoContent();
        }
        #endregion 
        #endregion
    }
}
