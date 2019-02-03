using System;
using System.Collections.Generic;

namespace OnlineBettingWebApi.Models
{
    public partial class Transaction
    {
        public Transaction(String type, int id, decimal? amount, DateTime date) {
            Type = type;
            IdWallet = id;
            Amount = amount;
            Date = date;
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public decimal? Amount { get; set; }
        public int? IdWallet { get; set; }
        public DateTime? Date { get; set; }

        public Wallet IdWalletNavigation { get; set; }
    }
}
