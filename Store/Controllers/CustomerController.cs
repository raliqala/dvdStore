using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using TypeLibrary.Interface;
using TypeLibrary.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ILogger _logger;
        private IDBHandler _handler;

        public CustomerController(ILogger<CustomerController> logger, IDBHandler handler)
        {
            _logger = logger;
            _handler = handler;
        }

        // POST api/<CustomerController>
        [HttpPost("/api/AddCustomer")]
        public ActionResult<SuccessResponse> AddCustomer(CustomerModel model)
        {
            Customer customer = new()
            {
                Name = model.Name,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
            };

            SuccessResponse success = new()
            {
                Success = true,
                Message = "Customer added Successfully",
            };

            if (_handler.BLL_AddCustomer(customer))
            {
                return success;
            }

            success.Success = false;
            success.Message = "Sorry could not add customer";
            return success;
        }

        // [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("/api/getCustomers")]
        public JsonResult GetCustomers()
        {
            var list = _handler.BLL_GetCustomers();

            SuccessResponse success = new()
            {
                Success = true,
                Message = "Sorry could not get customers",
            };

            if (list != null && list.Count != 0)
            {
                return new JsonResult(list);
            }

            return new JsonResult(success);
        }

        [HttpGet("/api/getCustomer/{id}")]
        public JsonResult GetCustomer(int id)
        {
            var customer = _handler.BLL_GetCustomer(id);

            SuccessResponse success = new()
            {
                Success = true,
                Message = "Sorry could not get a customer",
            };

            if (customer != null)
            {
                return new JsonResult(customer);
            }

            return new JsonResult(success);
        }

        [HttpPut("/api/updateCustomer/{id}")]
        public ActionResult<SuccessResponse> UpdateCustomer(int id, CustomerModel model)
        {
            UpdateCustomer customer = new()
            {
                Name = model.Name,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Id = id,
            };

            SuccessResponse success = new()
            {
                Success = true,
                Message = "Customer updated Successfully",
            };

            if (_handler.BLL_UpdateCustomer(customer))
            {
                return success;
            }

            success.Success = false;
            success.Message = "Sorry could not update customer";
            return success;
        }
    }
}
