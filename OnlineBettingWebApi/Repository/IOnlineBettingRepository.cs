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

        Task<List<TicketViewModel>> GetTickets();

        Task<int> AddTicket(Ticket ticket);

        Task<WalletViewModel> GetWallet();

        Task<WalletViewModel> UpdateWallet(WalletViewModel wallet);
    }
}
