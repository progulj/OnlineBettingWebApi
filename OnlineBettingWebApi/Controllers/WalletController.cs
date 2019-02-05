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
        IWalletRepository walletRepository;
        public WalletController(IWalletRepository _walletRepository)
        {
            walletRepository = _walletRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetWallet()
        {

            try
            {
                var wallet = await walletRepository.GetWallet();

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
                    var updatedWallet = await walletRepository.UpdateWallet(model);

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