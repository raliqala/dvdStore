using Microsoft.AspNetCore.Mvc;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Reflection;
using TypeLibrary.Interface;
using TypeLibrary.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private ILogger _logger;
        private IDBHandler _handler;

        public RentalController(ILogger<RentalController> logger, IDBHandler handler)
        {
            _logger = logger;
            _handler = handler;

        }

        private void IsOverDue()
        {
            var list = _handler.BLL_GetRentalList();
            DateTime currentDate = DateTime.Now;

            if (list != null && list.Count != 0)
            {
                foreach (var item in list)
                {
                    DateTime ovDate = DateTime.Parse(item.returnDate);
                    if (currentDate.Date >= ovDate.Date)
                    {
                        if (item.status != "returned")
                        {
                            UpdateRental rental = new()
                            {
                                dateReturned = "",
                                status = "overdue",
                                overDueAmount = 4,
                                Id = item.Id,
                            };
                            _handler.BLL_UpdateRental(rental);
                        }
                    }
                }
            }
        }

        // POST api/<RentalController>
        // rDate formart 2022-11-12
        [HttpPost("/api/addRental/{dvdId}/{customerId}/{rDate}")]
        public ActionResult<SuccessResponse> AddRental(int dvdId, int customerId, string rDate)
        {

            SuccessResponse success = new()
            {
                Success = true,
                Message = "Rental added Successfully",
            };

            var dvd = _handler.BLL_GetDvdById(dvdId);

            if (dvd != null)
            {
                if (dvd.Status == "unavailable" || dvd.Quantity <= 0)
                {
                    success.Success = false;
                    success.Message = "Sorry this dvd is out of stock";
                    return success;
                }
            }

            DateTime date = DateTime.Now;

            string[] dates = rDate.Split('-');
            DateTime returnDate = new DateTime(Int32.Parse(dates[0]), Int32.Parse(dates[1]), Int32.Parse(dates[2]));

            Rental rental = new()
            {
                dvdId = dvdId,
                customerId = customerId,
                rentalDate = date.ToString(),
                returnDate = returnDate.ToString(),
                status = "in_progress",
            };

            if (_handler.BLL_AddRental(rental))
            {
                return success;
            }

            success.Success = false;
            success.Message = "Sorry could not add rental";
            return success;
        }

        [HttpGet("/api/getRentals")]
        public JsonResult GetRentals()
        {
            this.IsOverDue();

            var list = _handler.BLL_GetRentalList();

            SuccessResponse success = new()
            {
                Success = true,
                Message = "Sorry could not get rentals",
            };

            if (list != null && list.Count != 0)
            {
                return new JsonResult(list);
            }

            return new JsonResult(success);
        }


        [HttpGet("/api/getRental/{id}")]
        public JsonResult GetRental(int id)
        {
            this.IsOverDue();
            var rent = _handler.BLL_GetRentalById(id);

            SuccessResponse success = new()
            {
                Success = true,
                Message = "Sorry could not get rental",
            };

            if (rent != null)
            {
                return new JsonResult(rent);
            }

            return new JsonResult(success);
        }

        [HttpGet("/api/getRentalsByStatus/{status}")]
        public JsonResult GetRentalsByStatus(string status)
        {
            this.IsOverDue();
            var dvd = _handler.BLL_GetRentalByStatus(status);

            SuccessResponse success = new()
            {
                Success = true,
                Message = "Sorry could not get rentals",
            };

            if (dvd != null && dvd.Count != 0)
            {
                return new JsonResult(dvd);
            }

            return new JsonResult(success);
        }

        [HttpPut("/api/updateRental/{id}")]
        public ActionResult<SuccessResponse> UpdateRental(int id, CustomerRental model)
        {
            DateTime date = DateTime.Now;
            UpdateRental rental = new()
            {
                dateReturned = model.status == "returned" ? date.ToString() : "",
                status = model.status,
                overDueAmount = model.overDueAmount,
                Id = id,
            };

            SuccessResponse success = new()
            {
                Success = true,
                Message = "Rental updated Successfully",
            };

            // Possible syntax issues here
            // _target = Field System.Delegate._target is not available in this context.
            //But it works
            if (_handler.BLL_UpdateRental(rental))
            {
                return success;
            }

            success.Success = false;
            success.Message = "Sorry could not update rental";
            return success;
        }


    }
}
