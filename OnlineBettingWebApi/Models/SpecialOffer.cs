using System;
using System.Collections.Generic;

namespace OnlineBettingWebApi.Models
{
    public partial class SpecialOffer
    {
        public int Id { get; set; }
        public string OddsHome { get; set; }
        public string OddsDraw { get; set; }
        public string OddsAway { get; set; }
        public string OddsDrawHome { get; set; }
        public string OddsDrawAway { get; set; }
        public string OddsHomeAway { get; set; }
        public int? IdOffer { get; set; }

        public Offer IdOfferNavigation { get; set; }
    }
}
