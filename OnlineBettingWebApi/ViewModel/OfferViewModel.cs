using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBettingWebApi.Models;

namespace OnlineBettingWebApi.ViewModel
{
    public class OfferViewModel
    {

        public OfferViewModel(Offer offer, SpecialOffer special)
        {

            Id = offer.Id;
            Game = offer.Game;
            Special = offer.Special;
            OddsFor1 = !String.IsNullOrEmpty(offer.OddsHome) ? offer.OddsHome : "-";
            OddsForX = !String.IsNullOrEmpty(offer.OddsDraw) ? offer.OddsDraw : "-";
            OddsFor2 = !String.IsNullOrEmpty(offer.OddsAway) ? offer.OddsAway : "-";
            OddsForX1 = !String.IsNullOrEmpty(offer.OddsDrawHome) ? offer.OddsDrawHome : "-";
            OddsForX2 = !String.IsNullOrEmpty(offer.OddsDrawAway) ? offer.OddsDrawAway : "-";
            OddsFor12 = !String.IsNullOrEmpty(offer.OddsHomeAway) ? offer.OddsHomeAway : "-";
            SpecialOddsFor1 = special != null ? !String.IsNullOrEmpty(special.OddsHome) ? special.OddsHome : "-" : "-";
            SpecialOddsForX = special != null ? !String.IsNullOrEmpty(special.OddsDraw) ? special.OddsDraw : "-" : "-";
            SpecialOddsFor2 = special != null ? !String.IsNullOrEmpty(special.OddsAway) ? special.OddsAway : "-" : "-";
            SpecialOddsForX1 = special != null ? !String.IsNullOrEmpty(special.OddsDrawHome) ? special.OddsDrawHome : "-" : "-";
            SpecialOddsForX2 = special != null ? !String.IsNullOrEmpty(special.OddsDrawAway) ? special.OddsDrawAway : "-" : "-";
            SpecialOddsFor12 = special != null ? !String.IsNullOrEmpty(special.OddsHomeAway) ? special.OddsHomeAway : "-" : "-";
                                     
        }
        public int Id { get; set; }
        public string Game { get; set; }
        public string OddsFor1 { get; set; }
        public string OddsForX { get; set; }
        public string OddsFor2 { get; set; }
        public string OddsForX1 { get; set; }
        public string OddsForX2 { get; set; }
        public string OddsFor12 { get; set; }
        public bool? Special { get; set; }
        public string SpecialOddsFor1 { get; set; }
        public string SpecialOddsForX { get; set; }
        public string SpecialOddsFor2 { get; set; }
        public string SpecialOddsForX1 { get; set; }
        public string SpecialOddsForX2 { get; set; }
        public string SpecialOddsFor12 { get; set; }
    }
}
