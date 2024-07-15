using FinancialInstrumentPrices.Common.Models;
using FinancialInstrumentPrices.Common.Repository;
using System.Collections.Concurrent;

namespace FinancialInstrumentPrices.Infrastructure.Repository
{
    public class SymbolRepository : ISymbolRepository
    {
        private readonly ConcurrentDictionary<string, PriceModel> _symbolsPrice = new();

        public IEnumerable<PriceModel> GetQuotesBySymbolCodes(List<string> symbols)
        {
            return _symbolsPrice.Values.Where(a => symbols.Any(symbol => symbol == a.Symbol)).ToList();
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