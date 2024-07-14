    
namespace FinancialInstrumentPrices.Common.Services
{
    public interface IForexService
    {
        Task SubscribeToSymbolsPrice(CancellationToken cancellationToken);
    }
}