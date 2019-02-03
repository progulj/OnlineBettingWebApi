using System;
using System.Collections.Generic;

namespace OnlineBettingWebApi.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal? Amount { get; set; }
        public int? IdWallet { get; set; }
        public DateTime? Date { get; set; }

        public Wallet IdWalletNavigation { get; set; }
    }
}
