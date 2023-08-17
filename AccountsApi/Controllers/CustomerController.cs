using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Commands.DeleteCustomers;
using Application.Customers.Commands.UpdateCustomer;
using Application.Customers.Queries.GetAllCustomers;
using Application.Customers.Queries.GetCustomerById;
using Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        #region Private Readonly Properties
        private readonly ISender Sender;

        #endregion
        public CustomerController(ISender sender) => Sender = sender;
        #region Constructor

        #endregion

        #region Web Actions

        // GET /api/customers
        #region Get All
        [HttpGet]
        public async Task<ActionResult<List<CustomerDTO>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await Sender.Send(new GetAllCustomersQuery(), cancellationToken);
            return Ok(result.Data);
        }
        #endregion

        // GET /api/customers/{id}
        #region Get By Id
        [HttpGet]
        [Route("{id}", Name = "GetById")]
        public async Task<ActionResult<List<CustomerDTO>>> GetById(string id, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(new GetCustomerByIdQuery(id), cancellationToken);
            return Ok(result.Data);
        }
        #endregion

        // POST /api/customers
        #region Create
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCustomerDTO request, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(new CreateCustomerCommand(request), cancellationToken);

            return CreatedAtRoute("GetById", new { id = result.Data }, result.Data);
        }
        #endregion

        // DELETE /api/customers
        #region Delete
        [HttpDelete]
        public async Task<ActionResult> DeleteCustomers(string[] ids, CancellationToken cancellationToken)
        {
            await Sender.Send(new DeleteCustomersCommand(ids), cancellationToken);
            return NoContent();
        }
        #endregion

        // PUT /api/customers
        #region Update
        [HttpPut]
        public async Task<ActionResult> Update(UpdateCustomerDTO request, CancellationToken cancellationToken)
        {
            await Sender.Send(new UpdateCustomerCommand(request), cancellationToken);
            return Ok();
        }
        #endregion 

        #endregion
    }
}
