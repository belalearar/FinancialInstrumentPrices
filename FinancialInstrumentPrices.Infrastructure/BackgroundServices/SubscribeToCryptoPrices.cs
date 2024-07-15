using FinancialInstrumentPrices.Common.Services;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace FinancialInstrumentPrices.Infrastructure.Services
{
    [ExcludeFromCodeCoverage]
    public class SubscribeToCryptoPrices(ICryptoService symbolRepository) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await symbolRepository.SubscribeToSymbolsPrice(stoppingToken);
        }
    }
}