using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBettingWebApi.Models;
using OnlineBettingWebApi.ViewModel;
using OnlineBettingWebApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace OnlineBettingWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        IOnlineBettingRepository onlineBettingRepository;
        public WalletController(IOnlineBettingRepository _onlineBettingRepository)
        {
            onlineBettingRepository = _onlineBettingRepository;
        }

        [HttpGet]
        [Route("GetWallet")]
        public async Task<IActionResult> GetWallet()
        {

            try
            {
                var wallet = await onlineBettingRepository.GetWallet();

                if (wallet == null)
                {
                    return NotFound();
                }

                return Ok(wallet);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("UpdateWallet")]
        public async Task<IActionResult> UpdatePost([FromBody]WalletView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await onlineBettingRepository.UpdateWallet(model);

                    return Ok(model);
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName ==
                             "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

    }

    [Route("[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        IOnlineBettingRepository onlineBettingRepository;
        public OffersController(IOnlineBettingRepository _onlineBettingRepository)
        {
            onlineBettingRepository = _onlineBettingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetOffers()
        {

            try
            {
                var offers = await onlineBettingRepository.GetOffers();

                if (offers == null)
                {
                    return NotFound();
                }

                return Ok(offers);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }

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

            return BadRequest();
        }

    }
}