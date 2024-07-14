using FinancialInstrumentPrices.Common.Services;
using Microsoft.Extensions.Hosting;

namespace FinancialInstrumentPrices.Infrastructure.Services
{
    public class SubscribeToForexPrices(IForexService forexService) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await forexService.SubscribeToSymbolsPrice(stoppingToken);
        }
    }
}