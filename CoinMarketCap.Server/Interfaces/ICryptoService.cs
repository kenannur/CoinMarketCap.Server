using System;
using System.Threading.Tasks;

namespace CoinMarketCap.Server.Interfaces
{
    public interface ICryptoService
    {
        Task<string> GetCoinPricesAsync();
    }
}
