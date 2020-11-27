using System;
using System.Threading.Tasks;
using CoinMarketCap.Server.Models.Request;

namespace CoinMarketCap.Server.Interfaces
{
    public interface IAccountService
    {
        Task<string> LoginAsync(LoginRequest request);
    }
}
