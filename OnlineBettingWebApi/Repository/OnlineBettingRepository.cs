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
        public static String DEBIT = "DEBIT";
        public static String CREDIT = "CREDIT";
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

                return (WalletViewModel)wallet;

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

                updateWaletBalance(wallet, walletView.Amount, CREDIT);

                //Commit the transaction
                await db.SaveChangesAsync();


                return (WalletViewModel)wallet;

            }

            return null;
        }



        public async Task<List<OfferViewModel>> GetOffers()
        {
            if (db != null)
            {
                return await (
                              from offer in db.Offer
                              where !db.WinOffer.Any(win => win.IdOffer == offer.Id)
                              from special in db.SpecialOffer.Where(special => special.IdOffer == offer.Id).DefaultIfEmpty()
                              select new OfferViewModel()
                              {
                                  Id = offer.Id,
                                  Game = offer.Game,
                                  Special = offer.Special,
                                  OddsFor1 = !String.IsNullOrEmpty(offer.OddsHome) ? offer.OddsHome : "-",
                                  OddsForX = !String.IsNullOrEmpty(offer.OddsDraw) ? offer.OddsDraw : "-",
                                  OddsFor2 = !String.IsNullOrEmpty(offer.OddsAway) ? offer.OddsAway : "-",
                                  OddsForX1 = !String.IsNullOrEmpty(offer.OddsDrawHome) ? offer.OddsDrawHome : "-",
                                  OddsForX2 = !String.IsNullOrEmpty(offer.OddsDrawAway) ? offer.OddsDrawAway : "-",
                                  OddsFor12 = !String.IsNullOrEmpty(offer.OddsHomeAway) ? offer.OddsHomeAway : "-",
                                  SpecialOddsFor1 = !String.IsNullOrEmpty(special.OddsHome) ? special.OddsHome : "-",
                                  SpecialOddsForX = !String.IsNullOrEmpty(special.OddsDraw) ? special.OddsDraw : "-",
                                  SpecialOddsFor2 = !String.IsNullOrEmpty(special.OddsAway) ? special.OddsAway : "-",
                                  SpecialOddsForX1 = !String.IsNullOrEmpty(special.OddsDrawHome) ? special.OddsDrawHome : "-",
                                  SpecialOddsForX2 = !String.IsNullOrEmpty(special.OddsDrawAway) ? special.OddsDrawAway : "-",
                                  SpecialOddsFor12 = !String.IsNullOrEmpty(special.OddsHomeAway) ? special.OddsHomeAway : "-"
                              }).ToListAsync();
            }

            return null;
        }

        public async Task<int> AddTicket(Ticket ticket)
        {
            Wallet wallet;

            if (db != null)
            {
                wallet = await db.Wallet.SingleOrDefaultAsync(w => w.Id == ticket.IdWallet);

                if (wallet.WalletBalance >= ticket.FullPayment)
                {
                    wallet.WalletBalance = wallet.WalletBalance - ticket.FullPayment;
                    wallet.Date = DateTime.Now;

                    updateWaletBalance(wallet, ticket.FullPayment, DEBIT);

                    await db.Ticket.AddAsync(ticket);
                    await db.SaveChangesAsync();

                    return ticket.Id;
                }
                else
                {
                    return 0;
                }

            }

            return 0;
        }

        public async Task<List<TicketViewModel>> GetTickets()
        {
            List<TicketViewModel> tickets = await db.Ticket.Include(ticket => ticket.Game).Select(t => (TicketViewModel)t).ToListAsync();

            return SetTicketStatus(tickets); ;
        }

        private List<TicketViewModel> SetTicketStatus(List<TicketViewModel> tickets)
        {
            List<WinOffer> winOffer = db.WinOffer.ToList();
            List<TicketViewModel> ticketsWithStatus = new List<TicketViewModel>();
            foreach (TicketViewModel ticket in tickets)
            {
                int countSucces = 0;
                int countFail = 0;

                foreach (GameViewModel game in ticket.Games)
                {
                    if (winOffer.Any(win => win.IdOffer == game.IdOffer))
                    {
                        if (winOffer.Any(win => win.IdOffer == game.IdOffer &&  win.Type == game.OddsType))
                        {
                            countSucces++;
                        }
                        else
                        {
                            countFail++;
                        }
                    }
                }
                if (countSucces > 0 || countFail > 0)
                {
                    if (countFail > 0)
                    {
                        ticket.Status = "2";
                    }
                    else if (countSucces == ticket.Games.Count())
                    {
                        ticket.Status = "1";
                    }
                }
                ticketsWithStatus.Add(ticket);
            }

            return ticketsWithStatus;
        }

        private void updateWaletBalance(Wallet wallet, decimal? amount, String type)
        {
            Transaction transaction;
            //Update wallet
            db.Wallet.Update(wallet);

            transaction = new Transaction
            {
                Type = type,
                IdWallet = wallet.Id,
                Amount = amount,
                Date = DateTime.Now
            };

            //log transaction
            db.Transaction.Add(transaction);


        }
    }
}
