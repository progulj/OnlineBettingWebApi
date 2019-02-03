using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBettingWebApi.ViewModel
{
    public class OfferViewModel
    {
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
