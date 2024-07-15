
using FinancialInstrumentPrices.Common.Models;

namespace FinancialInstrumentPrices.Common.Repository
{
    public interface ISymbolRepository
    {
        IEnumerable<PriceModel> GetQuotesBySymbolCodes(List<string> symbols);
        void UpdateSymbolPrice(PriceModel symbolPrice);
    }
}