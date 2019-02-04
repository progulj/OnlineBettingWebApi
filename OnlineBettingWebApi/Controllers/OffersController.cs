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

}