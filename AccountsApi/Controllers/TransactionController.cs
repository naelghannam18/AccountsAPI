using Application.Transactions.Commands.CreateTransaction;
using Application.Transactions.Commands.DeleteTransactions;
using Application.Transactions.Queries.GetAllAccountTransactions;
using Application.Transactions.Queries.GetTransactionById;
using Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AccountsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    #region Private Readonly Fields
    private readonly ISender Sender;
    #endregion

    #region Constructor
    public TransactionController(ISender sender)
    {
        Sender = sender;
    }
    #endregion

    #region Web Actions
    #region Get Transactions By Account
    [HttpGet]
    [Route("/api/accounts/{accountId}/Transactions")]
    public async Task<ActionResult<List<TransactionDTO>>> GetTransactionsByAccount(string accountId, CancellationToken cancellationToken)
    {
        var results = await Sender.Send(new GetAllAccountTransactionsQuery(accountId), cancellationToken);

        return Ok(results.Data);
    }
    #endregion

    #region Get Transaction By Id
    [HttpGet]
    [Route("{id}", Name = "GetTransactionById")]
    public async Task<ActionResult<TransactionDTO>> GetTransaction(string id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetTransactionByIdQuery(id), cancellationToken);

        return Ok(result.Data);
    }
    #endregion

    #region Create Transaction
    [HttpPost]
    public async Task<ActionResult> CreateTransaction([FromBody] TransactionDTO request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new CreateTransactionCommand(request), cancellationToken);

        return CreatedAtRoute("GetTransactionById", new { id = result.Data }, result.Data);
    }
    #endregion

    #region Delete Transactions
    [HttpDelete]
    public async Task<ActionResult> DeleteTransactions(string[] ids, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteTransactionsCommand(ids), cancellationToken);
        return NoContent();
    }
    #endregion
    #endregion
}
