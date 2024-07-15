using FinancialInstrumentPrices.Api.Hubs;
using FinancialInstrumentPrices.Common.ChannelArgs;
using FinancialInstrumentPrices.Common.Models;
using FinancialInstrumentPrices.Common.Repository;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Channels;

namespace FinancialInstrumentPrices.Api.Listeners
{
    [ExcludeFromCodeCoverage]
    public class PriceChannelListener(Channel<PriceChannelArgs> channel,
        ILogger<PriceChannelListener> logger,
        ISymbolRepository symbolRepository,
        IHubContext<PriceHub> priceHub) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var symbolPrice = await channel.Reader.ReadAsync(stoppingToken);
                    symbolRepository.UpdateSymbolPrice(new PriceModel
                    {
                        LastPrice = symbolPrice.LastPrice,
                        Symbol = symbolPrice.Symbol,
                        TickTime = symbolPrice.TickTime
                    });
                    await priceHub.Clients.Group(symbolPrice.Symbol).SendAsync("PriceUpdate", symbolPrice, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "PriceChannelListener:: Error processing symbol prices. Error: {error}, StackTrace: {st}", ex.Message, ex.StackTrace);
            }
        }
    }
}
