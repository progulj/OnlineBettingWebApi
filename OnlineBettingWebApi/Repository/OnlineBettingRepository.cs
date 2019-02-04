using Microsoft.EntityFrameworkCore;
using OnlineBettingWebApi.Models;
using OnlineBettingWebApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBettingWebApi.Repository
{
    public class OnlineBettingRepository : IOnlineBettingRepository
    {
        OnlineBettingContext db;
        public OnlineBettingRepository(OnlineBettingContext _db)
        {
            db = _db;
        }

        public async Task<WalletViewModel> GetWallet()
        {
            Wallet wallet;

            if (db != null)
            {
                wallet = await db.Wallet.LastOrDefaultAsync();

                return (WalletViewModel) wallet;

            }

            return null;
        }


        public async Task<WalletViewModel> UpdateWallet(WalletViewModel walletView)
        {
            Wallet wallet;


            if (db != null)
            {
                wallet = await db.Wallet.SingleOrDefaultAsync(w => w.Id == walletView.Id);

                wallet.WalletBalance = wallet.WalletBalance + walletView.Amount;
                wallet.Date = DateTime.Now;

                updateWaletBalance(wallet, walletView.Amount);

                //Commit the transaction
                await db.SaveChangesAsync();
                

                return (WalletViewModel) wallet;

            }

            return null;
        }

 

        public async Task<List<OfferViewModel>> GetOffers()
        {
            if (db != null)
            {
                return await (
                              from offers in db.Offer
                              where !db.WinOffer.Any(wins => wins.IdOffer == offers.Id)
                              from specials in db.SpecialOffer.Where(specials => specials.IdOffer == offers.Id).DefaultIfEmpty()
                              select new OfferViewModel
                              {
                                  Id = offers.Id,
                                  Game = offers.Game,
                                  Special = offers.Special,
                                  OddsFor1 = !String.IsNullOrEmpty(offers.OddsHome) ? offers.OddsHome : "-",
                                  OddsForX = !String.IsNullOrEmpty(offers.OddsDraw) ? offers.OddsDraw : "-",
                                  OddsFor2 = !String.IsNullOrEmpty(offers.OddsAway) ? offers.OddsAway : "-",
                                  OddsForX1 = !String.IsNullOrEmpty(offers.OddsDrawHome) ? offers.OddsDrawHome : "-",
                                  OddsForX2 = !String.IsNullOrEmpty(offers.OddsDrawAway) ? offers.OddsDrawAway : "-",
                                  OddsFor12 = !String.IsNullOrEmpty(offers.OddsHomeAway) ? offers.OddsHomeAway : "-",
                                  SpecialOddsFor1 = !String.IsNullOrEmpty(specials.OddsHome) ? specials.OddsHome : "-",
                                  SpecialOddsForX = !String.IsNullOrEmpty(specials.OddsDraw) ? specials.OddsDraw : "-",
                                  SpecialOddsFor2 = !String.IsNullOrEmpty(specials.OddsAway) ? specials.OddsAway : "-",
                                  SpecialOddsForX1 = !String.IsNullOrEmpty(specials.OddsDrawHome) ? specials.OddsDrawHome : "-",
                                  SpecialOddsForX2 = !String.IsNullOrEmpty(specials.OddsDrawAway) ? specials.OddsDrawAway : "-",
                                  SpecialOddsFor12 = !String.IsNullOrEmpty(specials.OddsHomeAway) ? specials.OddsHomeAway : "-",
                              }).ToListAsync();
            }

            return null;
        }

        public async Task<int> AddTicket(Ticket ticket)
        {

            if (db != null)
            {

                await db.Ticket.AddAsync(ticket);
                await db.SaveChangesAsync();

                return ticket.Id;
            }

            return 0;
        }

        public async Task<List<TicketViewModel>> GetTickets()
        {
            return await db.Ticket.Include(ticket => ticket.Game).Select(t => (TicketViewModel)t).ToListAsync();
        }

        private void updateWaletBalance(Wallet wallet, decimal? amount)
        {
            Transaction transaction;
            //Update wallet
            db.Wallet.Update(wallet);

            transaction = new Transaction
            {
                Type = Utilities.Util.CREDIT,
                IdWallet = wallet.Id,
                Amount = amount,
                Date = DateTime.Now
            };

            //log transaction
            db.Transaction.Add(transaction);


        }
    }
}
