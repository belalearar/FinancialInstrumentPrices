using FinancialInstrumentPrices.Common.Services;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace FinancialInstrumentPrices.Infrastructure.Services
{
    /// <summary>
    /// This Service will subscribe to forex prices and update the local cache for crypro
    /// prices.
    /// <param name="forexService"></param>
    ///</summary>
    [ExcludeFromCodeCoverage]
    public class SubscribeToForexPrices(IForexService forexService) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await forexService.SubscribeToSymbolsPrice(stoppingToken);
        }
    }
}