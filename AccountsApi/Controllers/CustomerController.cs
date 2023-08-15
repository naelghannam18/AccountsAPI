using Core.Services.Contracts;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AccountsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        #region Private Readonly Properties
        private ICustomerService Service { get; set; }

        #endregion

        #region Constructor
        public CustomerController(ICustomerService service)
        {
            Service = service;
        }
        #endregion

        #region Web Actions

        // GET /api/customers
        #region Get All
        [HttpGet]
        public async Task<ActionResult<List<CustomerDTO>>> GetAll()
        {
            var result = await Service.GetAllCustomers();
            return result.Status switch
            {
                HttpStatusCode.OK => Ok(result.Data)
            };
        }
        #endregion

        // GET /api/customers/{id}
        #region Get By Id
        [HttpGet]
        [Route("{id}", Name = "GetById")]
        public async Task<ActionResult<List<CustomerDTO>>> GetById(string id)
        {
            var result = await Service.GetCustomerById(id);

            return result.Status switch
            {
                HttpStatusCode.OK => Ok(result.Data),
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.BadRequest => BadRequest(result.Message)
            };
        }
        #endregion

        // POST /api/customers
        #region Create
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCustomerDTO request)
        {
            var result = await Service.CreateCustomer(request);

            return result.Status switch
            {
                HttpStatusCode.Created => CreatedAtRoute("GetById", new { id = result.Data }, result.Data),
                _ => StatusCode(500, result.Message)
            };
        }
        #endregion

        // DELETE /api/customers
        #region Delete
        [HttpDelete]
        public async Task<ActionResult> DeleteCustomers(string[] ids)
        {
            await Service.DeleteCustomers(ids);
            return NoContent();
        }
        #endregion

        // PUT /api/customers
        #region Update
        [HttpPut]
        public async Task<ActionResult> Update(UpdateCustomerDTO request)
        {
            var result = await Service.UpdateCustomer(request);

            return result.Status switch
            {
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.NotFound => NotFound(),
                _ => StatusCode(500, result.Message)
            };
        }
        #endregion 

        #endregion
    }
}
