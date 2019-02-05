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
        IOfferRepository offerRepository;
        public OffersController(IOfferRepository _offerRepository)
        {
            offerRepository = _offerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetOffers()
        {

            try
            {
                var offers = await offerRepository.GetOffers();

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