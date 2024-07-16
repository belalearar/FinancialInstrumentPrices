using FinancialInstrumentPrices.Common.Services;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace FinancialInstrumentPrices.Infrastructure.Services
{
    /// <summary>
    /// This Service will subscribe to crypto prices and update the local cache for crypro 
    /// prices.
    /// </summary>
    /// <param name="symbolRepository"></param>
    [ExcludeFromCodeCoverage]
    public class SubscribeToCryptoPrices(ICryptoService symbolRepository) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await symbolRepository.SubscribeToSymbolsPrice(stoppingToken);
        }
    }
}