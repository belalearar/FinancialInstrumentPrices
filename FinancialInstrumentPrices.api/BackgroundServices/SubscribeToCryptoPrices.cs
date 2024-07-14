
namespace FinancialInstrumentPrices.api.Services
{
    public class SubscribeToCryptoPrices(ICryptoService symbolRepository) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await symbolRepository.SubscribeToSymbolsPrice(stoppingToken);
        }
    }
}