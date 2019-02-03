using System;
using System.Collections.Generic;

namespace OnlineBettingWebApi.Models
{
    public partial class Wallet
    {
        public Wallet()
        {
            Ticket = new HashSet<Ticket>();
            Transaction = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string Account { get; set; }
        public decimal? WalletBalance { get; set; }
        public DateTime? Date { get; set; }

        public ICollection<Ticket> Ticket { get; set; }
        public ICollection<Transaction> Transaction { get; set; }
    }
}
