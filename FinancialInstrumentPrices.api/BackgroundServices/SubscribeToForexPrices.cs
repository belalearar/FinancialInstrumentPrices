
namespace FinancialInstrumentPrices.api.Services
{
    public class SubscribeToForexPrices(IForexService forexService) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await forexService.SubscribeToSymbolsPrice(stoppingToken);
        }
    }
}