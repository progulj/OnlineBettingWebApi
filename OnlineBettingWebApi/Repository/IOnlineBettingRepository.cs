using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBettingWebApi.Models;
using OnlineBettingWebApi.ViewModel;

namespace OnlineBettingWebApi.Repository
{
    public  interface IOnlineBettingRepository
    {
        Task<List<OfferViewModel>> GetOffers();

        /**Task<List<TicketViewModel>> GetTickets();**/

        Task<int> AddTicket(TicketViewModel ticket);

        Task<WalletView> GetWallet();

        Task<WalletView> UpdateWallet(WalletView wallet);
    }
}
