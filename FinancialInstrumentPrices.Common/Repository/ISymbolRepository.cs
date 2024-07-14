
using FinancialInstrumentPrices.Common.Models;

namespace FinancialInstrumentPrices.Common.Repository
{
    public interface ISymbolRepository
    {
        IEnumerable<PriceModel> GetQuotesBySymbolCodes(List<string> symbols);
        PriceModel? GetSymbolPrice(string symbol);
        void UpdateSymbolPrice(PriceModel symbolPrice);
    }
}