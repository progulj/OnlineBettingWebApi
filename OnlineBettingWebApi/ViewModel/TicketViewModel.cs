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
        public IList<GameViewModel> Games { get; set; }


        public static implicit operator TicketViewModel(Ticket ticket)
        {
            return new TicketViewModel
            {
                Id = ticket.Id,
                TotalOdds = ticket.TotalOdds,
                FullPayment = ticket.FullPayment,
                Commission = ticket.Commission,
                EstimatedWin = ticket.EstimatedWin,
                IdWallet = ticket.IdWallet,
                Date = ticket.Date,
                Games = getGames(ticket.Game),
                Status = null
            };
        }

        public static implicit operator Ticket(TicketViewModel ticketVM)
        {
            return new Ticket
            {
                Id = ticketVM.Id,
                TotalOdds = ticketVM.TotalOdds,
                FullPayment = ticketVM.FullPayment,
                Commission = ticketVM.Commission,
                EstimatedWin = ticketVM.EstimatedWin,
                IdWallet = ticketVM.IdWallet,
                Date = ticketVM.Date,
                Game = getGames(ticketVM.Games)
            };
        }


        private static IList<GameViewModel> getGames(ICollection<Game> games)
        {
            IList<GameViewModel> castedGames = new List<GameViewModel>();

            foreach (Game game in games)
            {
                castedGames.Add((GameViewModel)game);
            }
            return castedGames;
        }

        private static IList<Game> getGames(ICollection<GameViewModel> games)
        {
            IList<Game> castedGames = new List<Game>();

            foreach (GameViewModel game in games)
            {
                castedGames.Add((Game)game);
            }
            return castedGames;
        }
    }
}
