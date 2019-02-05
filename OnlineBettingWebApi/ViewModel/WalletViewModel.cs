using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBettingWebApi.Models;

namespace OnlineBettingWebApi.ViewModel
{
    public class WalletViewModel
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public decimal? WalletBalance { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }

        public static implicit operator WalletViewModel(Wallet wallet)
        {
            return new WalletViewModel
            {
                Id = wallet.Id,
                Account = wallet.Account,
                WalletBalance = wallet.WalletBalance,
                Amount = null,
                Date = wallet.Date
            };
        }

        public static implicit operator Wallet(WalletViewModel walletVM)
        {
            return new Wallet
            {
                Id = walletVM.Id,
                Account = walletVM.Account,
                WalletBalance = walletVM.WalletBalance,
                Date = walletVM.Date
            };
        }
    }
}
