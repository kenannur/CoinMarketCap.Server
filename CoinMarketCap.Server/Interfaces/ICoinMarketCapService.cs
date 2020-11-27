using System.Threading.Tasks;

namespace CoinMarketCap.Server.Interfaces
{
    public interface ICoinMarketCapService
    {
        Task<string> GetLatestDataAsync();
    }
}
