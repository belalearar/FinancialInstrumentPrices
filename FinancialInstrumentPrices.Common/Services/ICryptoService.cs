
using FinancialInstrumentPrices.Common.Models;

namespace FinancialInstrumentPrices.Common.Services
{
    public interface ICryptoService
    {
        Task<PriceModel?> GetSymbolPrice(string symbol);
        Task SubscribeToSymbolsPrice(CancellationToken cancellationToken);
    }
}