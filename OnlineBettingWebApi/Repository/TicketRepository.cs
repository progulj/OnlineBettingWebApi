using Microsoft.EntityFrameworkCore;
using OnlineBettingWebApi.Models;
using OnlineBettingWebApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBettingWebApi.Repository
{
    public class TicketRepository : ITicketRepository
    {
        OnlineBettingContext db;

        public TicketRepository(OnlineBettingContext _db)
        {
            db = _db;
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

                    UpdateWaletBalance(wallet, ticket.FullPayment, "DEBIT");

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
            List<TicketViewModel> tickets = await db.Ticket.Include(ticket => ticket.Game).Select(t => (TicketViewModel)t).OrderByDescending(date => date.Date).ToListAsync();

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

        private void UpdateWaletBalance(Wallet wallet, decimal? amount, String type)
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
