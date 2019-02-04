using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBettingWebApi.Models;

namespace OnlineBettingWebApi.ViewModel
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public string Odds { get; set; }
        public string OddsType { get; set; }
        public string Name { get; set; }
        public bool? Special { get; set; }
        public int? IdTicket { get; set; }
        public int? IdOffer { get; set; }
        public DateTime? Date { get; set; }

        public static implicit operator GameViewModel(Game game)
        {
            return new GameViewModel
            {
                Id = game.Id,
                Odds = game.Odds,
                OddsType = game.OddsType,
                Name = game.Name,
                Special = game.Special,
                IdTicket = game.IdTicket,
                IdOffer = game.IdOffer,
                Date = game.Date
            };
        }

        public static implicit operator Game(GameViewModel game)
        {
            return new Game
            {
                Id = game.Id,
                Odds = game.Odds,
                OddsType = game.OddsType,
                Name = game.Name,
                Special = game.Special,
                IdTicket = game.IdTicket,
                IdOffer = game.IdOffer,
                Date = game.Date
            };
        }
    }


}
