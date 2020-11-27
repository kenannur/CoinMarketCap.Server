using System.Threading.Tasks;
using CoinMarketCap.Server.Interfaces;

namespace CoinMarketCap.Server.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly ICoinMarketCapService _coinMarketCapService;

        public CryptoService(ICoinMarketCapService coinMarketCapService)
        {
            _coinMarketCapService = coinMarketCapService;
        }

        public async Task<string> GetCoinPricesAsync()
        {
            var result = await _coinMarketCapService.GetLatestDataAsync();
            return result;
        }


    }
}
