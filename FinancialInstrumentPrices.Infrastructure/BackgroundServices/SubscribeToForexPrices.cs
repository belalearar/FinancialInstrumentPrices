using FinancialInstrumentPrices.Common.Services;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace FinancialInstrumentPrices.Infrastructure.Services
{
    [ExcludeFromCodeCoverage]
    public class SubscribeToForexPrices(IForexService forexService) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await forexService.SubscribeToSymbolsPrice(stoppingToken);
        }
    }
}