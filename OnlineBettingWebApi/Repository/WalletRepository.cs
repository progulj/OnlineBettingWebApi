using Microsoft.EntityFrameworkCore;
using OnlineBettingWebApi.Models;
using OnlineBettingWebApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBettingWebApi.Repository
{
    public class WalletRepository : IWalletRepository
    {
        
        OnlineBettingContext db;

        public WalletRepository(OnlineBettingContext _db)
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

                updateWaletBalance(wallet, walletView.Amount, "CREDIT");

                //Commit the transaction
                await db.SaveChangesAsync();


                return (WalletViewModel)wallet;

            }

            return null;
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
