
using FinancialInstrumentPrices.api.Models;

namespace FinancialInstrumentPrices.api.Repository
{
    public interface ISymbolRepository
    {
        PriceModel? GetSymbolPrice(string symbol);
        void UpdateSymbolPrice(PriceModel symbolPrice);
    }
}