
using FinancialInstrumentPrices.Common.Services;
using Microsoft.Extensions.Hosting;

namespace FinancialInstrumentPrices.Infrastructure.Services
{
    public class SubscribeToCryptoPrices(ICryptoService symbolRepository) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await symbolRepository.SubscribeToSymbolsPrice(stoppingToken);
        }
    }
}