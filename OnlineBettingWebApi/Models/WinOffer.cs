using System;
using System.Collections.Generic;

namespace OnlineBettingWebApi.Models
{
    public partial class WinOffer
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? IdOffer { get; set; }

        public Offer IdOfferNavigation { get; set; }
    }
}
