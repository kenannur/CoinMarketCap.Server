using System.Net;
using System.Threading.Tasks;
using CoinMarketCap.Server.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CoinMarketCap.Server.Extensions
{
    public static class HttpContextExtensions
    {
        public async static Task OverrideResponse(this HttpContext httpContext, HttpStatusCode code, string message = null)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)code;

            var apiResponse = new ApiResponse
            {
                IsSuccess = false,
                ErrorMessage = message
            };

            var jApiResponse = JsonConvert.SerializeObject(apiResponse, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            await httpContext.Response.WriteAsync(jApiResponse);
        }
    }
}
