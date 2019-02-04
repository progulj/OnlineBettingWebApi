using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBettingWebApi.Models;
using OnlineBettingWebApi.ViewModel;
using OnlineBettingWebApi.Repository;
using Microsoft.AspNetCore.Mvc;
using OnlineBettingWebApi.Utilities;

namespace OnlineBettingWebApi.Controllers
{
 
    [Route("[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        IOnlineBettingRepository onlineBettingRepository;
        public TicketsController(IOnlineBettingRepository _onlineBettingRepository)
        {
            onlineBettingRepository = _onlineBettingRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddTicket([FromBody]TicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                Validator validator = new Validator(model);
                if (validator.IsValid)
                {
                    try
                    {
                        int ticketId = await onlineBettingRepository.AddTicket(model);
                        if (ticketId > 0)
                        {
                            return Ok(ticketId);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    catch (Exception)
                    {

                        return BadRequest();
                    }
                }
                else {
                    return BadRequest(validator.GetViolations().FirstOrDefault());
                }

            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {

            try
            {
                var  tickets = await onlineBettingRepository.GetTickets();

                if (tickets == null)
                {
                    return NotFound();
                }

                return Ok(tickets);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}