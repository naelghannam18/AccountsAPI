using Core.Services.Contracts;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AccountsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        #region Private Readonly Fields
        private readonly ITransactionService TransactionService;
        #endregion

        #region Constructor
        public TransactionController(ITransactionService service)
        {
            TransactionService = service;
        }
        #endregion

        #region Web Actions
        #region Get Transactions By Account
        [HttpGet]
        [Route("/api/accounts/{accountId:int:min(1)}/Transactions")]
        public async Task<ActionResult<List<TransactionDTO>>> GetTransactionsByAccount(int accountId)
        {
            var results = await TransactionService.GetAllTransactions(accountId);

            return Ok(results.Data);
        }
        #endregion

        #region Get Transaction By Id
        [HttpGet]
        [Route("{id:int:min(1)}", Name = "GetTransactionById")]
        public async Task<ActionResult<TransactionDTO>> GetTransaction(int id)
        {
            var result = await TransactionService.GetTransactionById(id);

            return result.Status switch
            {
                HttpStatusCode.OK => Ok(result.Data),
                HttpStatusCode.NotFound => NotFound(),
                _ => StatusCode(500, result.Message)
            };
        }
        #endregion

        #region Create Transaction
        [HttpPost]
        public async Task<ActionResult> CreateTransaction([FromBody]TransactionDTO request)
        {
            var result = await TransactionService.CreateTransaction(request);

            return result.Status switch
            {
                HttpStatusCode.Created => CreatedAtRoute("GetTransactionById", new { id = result.Data }, result.Data),
                _ => StatusCode(500, result.Message)
            };
        }
        #endregion

        #region Delete Transactions
        [HttpDelete]
        public async Task<ActionResult> DeleteTransactions(int[] ids)
        {
            await TransactionService.DeleteTransaction(ids);
            return NoContent();
        }
        #endregion
        #endregion
    }
}
