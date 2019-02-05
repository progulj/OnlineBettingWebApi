using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBettingWebApi.Models;
using OnlineBettingWebApi.ViewModel;

namespace OnlineBettingWebApi.Repository
{
    public  interface IOfferRepository
    {

        Task<List<OfferViewModel>> GetOffers();
    }
}
