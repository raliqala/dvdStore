using Microsoft.AspNetCore.Mvc;
using Store.Models;
using System.Reflection;
using TypeLibrary.Interface;
using TypeLibrary.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DvdController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IDBHandler _handler;

        public DvdController(ILogger<DvdController> logger, IDBHandler handler)
        {
            _logger = logger;
            _handler = handler;

        }

        // POST api/<DvdController>
        [HttpPost("/api/AddDvd")]
        public ActionResult<SuccessResponse> AddDvd(DVD model)
        {
            DvdStorage dvdStorage = new()
            {
                Title = model.Title,
                Description = model.Description,
                RentalPrice = model.RentalPrice,
                Quantity = model.Quantity,
                Status = "available",
                adminId = 1,
                Slug = model.Title.Replace(" ", "-").ToLower(),
            };

            SuccessResponse success = new()
            {
                Success = true,
                Message = "Dvd added Successfully",
            };

            if (_handler.BLL_AddDVDStock(dvdStorage))
            {
                return success;
            }

            success.Success = false;
            success.Message = "Sorry could not add dvd";
            return success;
        }

        [HttpGet("/api/getDvdList")]
        public JsonResult ListDvd()
        {
            var list = _handler.BLL_GetDvdList();

            SuccessResponse success = new()
            {
                Success = true,
                Message = "Sorry could not get dvds",
            };

            if (list != null && list.Count != 0)
            {
                return new JsonResult(list);
            }

            return new JsonResult(success);
        }

        [HttpGet("/api/getDvd/{id}")]
        public JsonResult GetDvd(int id)
        {
            var dvd = _handler.BLL_GetDvdById(id);

            SuccessResponse success = new()
            {
                Success = true,
                Message = "Sorry could not get dvd",
            };

            if (dvd != null)
            {
                return new JsonResult(dvd);
            }

            return new JsonResult(success);
        }

        [HttpPut("/api/updateDvd/{id}")]
        public ActionResult<SuccessResponse> UpdateDvd(int id, DVD model)
        {
            UpdateDvd dvdStorage = new()
            {
                Title = model.Title,
                Description = model.Description,
                RentalPrice = model.RentalPrice,
                Quantity = model.Quantity,
                Status = model.Status,
                Id = id,
            };

            SuccessResponse success = new()
            {
                Success = true,
                Message = "Dvd updated Successfully",
            };

            if (_handler.BLL_UpdateDvd(dvdStorage))
            {
                return success;
            }

            success.Success = false;
            success.Message = "Sorry could not add dvd";
            return success;
        }
    }
}
