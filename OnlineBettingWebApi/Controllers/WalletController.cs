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
        public async Task<IActionResult> UpdatePost([FromBody]WalletViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedWallet = await onlineBettingRepository.UpdateWallet(model);

                    return Ok(updatedWallet);
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
}