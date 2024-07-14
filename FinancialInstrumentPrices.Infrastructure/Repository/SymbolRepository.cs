using FinancialInstrumentPrices.Common;
using FinancialInstrumentPrices.Common.Configs;
using FinancialInstrumentPrices.Common.Extensions;
using FinancialInstrumentPrices.Common.Models;
using FinancialInstrumentPrices.Common.Repository;
using FinancialInstrumentPrices.Infrastructure.Mapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Text.Json;

namespace FinancialInstrumentPrices.Infrastructure.Repository
{
    public class SymbolRepository(ILogger<SymbolRepository> logger, IOptions<ApiSettings> options, IOptions<HttpConfigs> httpOptions) : ISymbolRepository
    {
        private readonly ConcurrentDictionary<string, PriceModel> _symbolsPrice = new();
        private readonly ApiSettings _settings = options.Value;
        private readonly HttpConfigs _httpConfigs = httpOptions.Value;

        public IEnumerable<PriceModel> GetQuotesBySymbolCodes(List<string> symbols)
        {
            return _symbolsPrice.Values.Where(a => symbols.Any(symbol => symbol == a.Symbol)).ToList();
        }

        public async Task<PriceModel?> GetSymbolPrice(string symbol, CancellationToken cancellationToken)
        {
            string? path = GetPath(symbol);
            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.GetAsync(_httpConfigs.RestUrl + path + "/top?tickers=" + symbol + "&token=" + _settings.ApiKey, cancellationToken);
                result.EnsureSuccessStatusCode();
                var content = await result.Content.ReadAsStringAsync();
                var priceModel = JsonSerializer.Deserialize<List<PriceResponse>>(content, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                })!;
                return priceModel.First().ToPriceModel();
            }
        }

        public void UpdateSymbolPrice(PriceModel symbolPrice)
        {
            if (!_symbolsPrice.TryAdd(symbolPrice.Symbol, symbolPrice))
            {
                _symbolsPrice[symbolPrice.Symbol] = symbolPrice;
            }
        }

        private string? GetPath(string symbol)
        {
            string? path = null;
            var isForex = symbol.IsForex(_settings);
            if (isForex.HasValue)
            {
                if (isForex.Value)
                {
                    path = ApiConstants.Forex;
                }
                else
                {
                    path = ApiConstants.Crypto;
                }
            }

            return path;
        }
    }
}