
namespace FinancialInstrumentPrices.api.Services
{
    public interface ICryptoService
    {
        Task SubscribeToSymbolsPrice(CancellationToken cancellationToken);
    }
}