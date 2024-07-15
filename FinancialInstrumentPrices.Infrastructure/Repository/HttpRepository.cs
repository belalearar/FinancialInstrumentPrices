using FinancialInstrumentPrices.Common.Repository;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FinancialInstrumentPrices.Infrastructure.Repository
{
    public class HttpRepository(HttpClient httpClient, ILogger<HttpRepository> logger) : IHttpRepository
    {
        public async Task<T> GetAsync<T>(string url)
        {
            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                return result!;
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, "Error While Parsing Json {Url}", url);
                throw;
            }
        }
    }
}