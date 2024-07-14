
using FinancialInstrumentPrices.Common.Models;

namespace FinancialInstrumentPrices.Common.Repository
{
    public interface ISymbolRepository
    {
        IEnumerable<PriceModel> GetQuotesBySymbolCodes(List<string> symbols);
        Task<PriceModel?> GetSymbolPrice(string symbol, CancellationToken cancellationToken);
        void UpdateSymbolPrice(PriceModel symbolPrice);
    }
}