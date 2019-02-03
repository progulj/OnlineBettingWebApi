using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBettingWebApi.Models;

namespace OnlineBettingWebApi.ViewModel
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        public string TotalOdds { get; set; }
        public decimal? FullPayment { get; set; }
        public decimal? Commission { get; set; }
        public decimal? EstimatedWin { get; set; }
        public int? IdWallet { get; set; }
        public DateTime? Date { get; set; }
        public string Status { get; set; }
        public ICollection<Game> Games { get; set; }
        
    }
}
