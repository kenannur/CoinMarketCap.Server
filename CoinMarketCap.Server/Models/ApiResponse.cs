namespace CoinMarketCap.Server.Models
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }

        public object MainResponse { get; set; }

        public string ErrorMessage { get; set; }
    }
}
