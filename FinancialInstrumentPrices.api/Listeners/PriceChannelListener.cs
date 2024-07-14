using FinancialInstrumentPrices.api.ChannelArgs;
using FinancialInstrumentPrices.api.Repository;
using System.Threading.Channels;

namespace FinancialInstrumentPrices.api.Listeners
{
    public class PriceChannelListener(Channel<PriceChannelArgs> channel,
        ILogger<PriceChannelListener> logger,
        ISymbolRepository symbolRepository) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var symbolPrice = await channel.Reader.ReadAsync(stoppingToken);
                    symbolRepository.UpdateSymbolPrice(new Models.PriceModel
                    {
                        LastPrice = symbolPrice.LastPrice,
                        Symbol = symbolPrice.Symbol,
                        TickTime = symbolPrice.TickTime
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "PriceChannelListener:: Error processing symbol prices. Error: {error}, StackTrace: {st}", ex.Message, ex.StackTrace);
            }
        }
    }
}
