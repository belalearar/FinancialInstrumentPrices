
namespace FinancialInstrumentPrices.api.Services
{
    public interface IForexService
    {
        Task SubscribeToSymbolsPrice(CancellationToken cancellationToken);
    }
}