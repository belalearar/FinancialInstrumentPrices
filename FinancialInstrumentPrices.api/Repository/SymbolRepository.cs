using FinancialInstrumentPrices.api.Models;
using System.Collections.Concurrent;

namespace FinancialInstrumentPrices.api.Repository
{
    public class SymbolRepository(ILogger<SymbolRepository> logger) : ISymbolRepository
    {
        private readonly ConcurrentDictionary<string, PriceModel> _symbolsPrice = new();

        public PriceModel? GetSymbolPrice(string symbol)
        {
            if (!_symbolsPrice.TryGetValue(symbol, out var price))
            {
                logger.LogInformation("Symbol Not Found");
                return null;
            }
            return price;
        }

        public void UpdateSymbolPrice(PriceModel symbolPrice)
        {
            if (!_symbolsPrice.TryAdd(symbolPrice.Symbol, symbolPrice))
            {
                _symbolsPrice[symbolPrice.Symbol] = symbolPrice;
            }
        }
    }
}