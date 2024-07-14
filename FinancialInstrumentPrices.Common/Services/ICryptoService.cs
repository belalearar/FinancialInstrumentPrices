
namespace FinancialInstrumentPrices.Common.Services
{
    public interface ICryptoService
    {
        Task SubscribeToSymbolsPrice(CancellationToken cancellationToken);
    }
}