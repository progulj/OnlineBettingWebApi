using Microsoft.EntityFrameworkCore;
using OnlineBettingWebApi.Models;
using OnlineBettingWebApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBettingWebApi.Repository
{
    public class OfferRepository : IOfferRepository
    {

        OnlineBettingContext db;

        public OfferRepository(OnlineBettingContext _db)
        {
            db = _db;
        }


        public async Task<List<OfferViewModel>> GetOffers()
        {
            if (db != null)
            {
                return await (
                              from offer in db.Offer
                              where !db.WinOffer.Any(win => win.IdOffer == offer.Id)
                              from special in db.SpecialOffer.Where(special => special.IdOffer == offer.Id).DefaultIfEmpty()
                              select new OfferViewModel()
                              {
                                  Id = offer.Id,
                                  Game = offer.Game,
                                  Special = offer.Special,
                                  OddsFor1 = !String.IsNullOrEmpty(offer.OddsHome) ? offer.OddsHome : "-",
                                  OddsForX = !String.IsNullOrEmpty(offer.OddsDraw) ? offer.OddsDraw : "-",
                                  OddsFor2 = !String.IsNullOrEmpty(offer.OddsAway) ? offer.OddsAway : "-",
                                  OddsForX1 = !String.IsNullOrEmpty(offer.OddsDrawHome) ? offer.OddsDrawHome : "-",
                                  OddsForX2 = !String.IsNullOrEmpty(offer.OddsDrawAway) ? offer.OddsDrawAway : "-",
                                  OddsFor12 = !String.IsNullOrEmpty(offer.OddsHomeAway) ? offer.OddsHomeAway : "-",
                                  SpecialOddsFor1 = !String.IsNullOrEmpty(special.OddsHome) ? special.OddsHome : "-",
                                  SpecialOddsForX = !String.IsNullOrEmpty(special.OddsDraw) ? special.OddsDraw : "-",
                                  SpecialOddsFor2 = !String.IsNullOrEmpty(special.OddsAway) ? special.OddsAway : "-",
                                  SpecialOddsForX1 = !String.IsNullOrEmpty(special.OddsDrawHome) ? special.OddsDrawHome : "-",
                                  SpecialOddsForX2 = !String.IsNullOrEmpty(special.OddsDrawAway) ? special.OddsDrawAway : "-",
                                  SpecialOddsFor12 = !String.IsNullOrEmpty(special.OddsHomeAway) ? special.OddsHomeAway : "-"
                              }).ToListAsync();
            }

            return null;
        }

    }
}
