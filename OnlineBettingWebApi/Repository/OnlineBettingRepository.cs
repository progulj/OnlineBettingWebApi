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

        public async Task<WalletView> GetWallet()
        {
            Wallet wallet;
            WalletView walletView;
            if (db != null)
            {
                wallet = await db.Wallet.LastOrDefaultAsync();
                walletView = new WalletView
                {
                    Account = wallet.Account,
                    Id = wallet.Id,
                    WalletBalance = wallet.WalletBalance,
                    Date = wallet.Date,
                    Amount = null
                };

                return walletView;

            }

            return null;

        }


        public async Task<WalletView> UpdateWallet(WalletView walletView)
        {
            Wallet wallet;
            Transaction transaction;

            if (db != null)
            {
                wallet = await db.Wallet.SingleOrDefaultAsync(w => w.Id == walletView.Id);

                wallet.WalletBalance = wallet.WalletBalance + walletView.Amount;
                wallet.Date = DateTime.Now;

                walletView.WalletBalance = wallet.WalletBalance;
                walletView.Date = wallet.Date;

                //Update wallet
                db.Wallet.Update(wallet);

                transaction = new Transaction(Utilities.Util.CREDIT, wallet.Id, walletView.Amount, DateTime.Now);

                //log transaction
                db.Transaction.Add(transaction);

                //Commit the transaction
                await db.SaveChangesAsync();

                walletView.Amount = null;

                return walletView;

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
                              select new OfferViewModel(offer, special)).ToListAsync();
            }

            return null;
        }

        public async Task<int> AddTicket(TicketViewModel ticketView)
        {
            if (db != null)
            {
                
                //await db.Ticket.AddAsync();
                //await db.SaveChangesAsync();

                return 1;
            }

            return 0;
        }

    }
}