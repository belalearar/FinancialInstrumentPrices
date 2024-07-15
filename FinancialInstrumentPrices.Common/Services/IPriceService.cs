using FinancialInstrumentPrices.Common.Models;

namespace FinancialInstrumentPrices.Common.Services
{
    public interface IPriceService
    {
        Task<PriceModel?> GetSymbolPrice(string symbolCode, CancellationToken cancellationToken);
    }
}