using System;
using System.Collections.Generic;

namespace OnlineBettingWebApi.Models
{
    public partial class Offer
    {
        public Offer()
        {
            GameNavigation = new HashSet<Game>();
            SpecialOffer = new HashSet<SpecialOffer>();
            WinOffer = new HashSet<WinOffer>();
        }

        public int Id { get; set; }
        public string Game { get; set; }
        public string OddsHome { get; set; }
        public string OddsDraw { get; set; }
        public string OddsAway { get; set; }
        public string OddsDrawHome { get; set; }
        public string OddsDrawAway { get; set; }
        public string OddsHomeAway { get; set; }
        public bool? Special { get; set; }

        public ICollection<Game> GameNavigation { get; set; }
        public ICollection<SpecialOffer> SpecialOffer { get; set; }
        public ICollection<WinOffer> WinOffer { get; set; }
    }
}
