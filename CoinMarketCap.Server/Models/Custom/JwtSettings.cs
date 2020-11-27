namespace CoinMarketCap.Server.Models.Custom
{
    public class JwtSettings : IJwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireMinutesFromNow { get; set; }
    }

    public interface IJwtSettings
    {
        string SecretKey { get; set; }
        string Issuer { get; set; }
        string Audience { get; set; }
        int ExpireMinutesFromNow { get; set; }
    }
}
