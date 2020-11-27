using System.Net.Http;
using System.Threading.Tasks;
using CoinMarketCap.Server.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CoinMarketCap.Server.Services
{
    public class CoinMarketCapService : ICoinMarketCapService
    {
        private readonly HttpClient _httpClient;

        public CoinMarketCapService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", configuration.GetValue<string>("CoinMarketCapApiKey"));
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<string> GetLatestDataAsync()
        {
            var requestUri = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest";
            var jResponse = await _httpClient.GetStringAsync(requestUri);
            return jResponse;
        }
    }
}
