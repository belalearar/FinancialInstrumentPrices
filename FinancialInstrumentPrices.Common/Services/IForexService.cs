
using FinancialInstrumentPrices.Common.Models;

namespace FinancialInstrumentPrices.Common.Services
{
    public interface IForexService
    {
        Task<PriceModel?> GetSymbolPrice(string symbol);
        Task SubscribeToSymbolsPrice(CancellationToken cancellationToken);
    }
}