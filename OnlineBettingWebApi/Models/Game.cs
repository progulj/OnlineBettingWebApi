using System;
using System.Collections.Generic;

namespace OnlineBettingWebApi.Models
{
    public partial class Game
    {
        public int Id { get; set; }
        public string Odds { get; set; }
        public string OddsType { get; set; }
        public string Name { get; set; }
        public bool? Special { get; set; }
        public int? IdTicket { get; set; }
        public int? IdOffer { get; set; }
        public DateTime? Date { get; set; }

        public Offer IdOfferNavigation { get; set; }
        public Ticket IdTicketNavigation { get; set; }
    }
}
