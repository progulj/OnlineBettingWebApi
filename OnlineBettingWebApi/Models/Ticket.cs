using System;
using System.Collections.Generic;

namespace OnlineBettingWebApi.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            Game = new HashSet<Game>();
        }

        public int Id { get; set; }
        public string TotalOdds { get; set; }
        public decimal? FullPayment { get; set; }
        public decimal? Commission { get; set; }
        public decimal? EstimatedWin { get; set; }
        public int? IdWallet { get; set; }
        public DateTime? Date { get; set; }

        public Wallet IdWalletNavigation { get; set; }
        public ICollection<Game> Game { get; set; }
    }
}
