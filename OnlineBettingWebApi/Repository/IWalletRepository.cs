using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBettingWebApi.Models;
using OnlineBettingWebApi.ViewModel;

namespace OnlineBettingWebApi.Repository
{
    public  interface IWalletRepository
    {
        
        Task<WalletViewModel> GetWallet();

        Task<WalletViewModel> UpdateWallet(WalletViewModel wallet);
    }
}
