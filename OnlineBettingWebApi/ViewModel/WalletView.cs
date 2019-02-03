using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBettingWebApi.ViewModel
{
    public class WalletView
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public decimal? WalletBalance { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
    }
}
