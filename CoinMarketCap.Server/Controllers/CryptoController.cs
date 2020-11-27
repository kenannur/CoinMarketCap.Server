using System.Threading.Tasks;
using CoinMarketCap.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoinMarketCap.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CryptoController : ControllerBase
    {
        private readonly ICryptoService _cryptoService;

        public CryptoController(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [ActionName("GetCoinPrices")]
        public async Task<IActionResult> GetCoinPricesAsync()
        {
            var result = await _cryptoService.GetCoinPricesAsync();
            return Ok(result);
        }
    }
}
