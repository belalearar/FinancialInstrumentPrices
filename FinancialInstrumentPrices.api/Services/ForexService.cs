using FinancialInstrumentPrices.api.Configs;
using FinancialInstrumentPrices.api.Models;
using Microsoft.Extensions.Options;

namespace FinancialInstrumentPrices.api.Services
{
    public class ForexService(ILogger<ForexService> logger, IOptions<HttpConfigs> httpOptions,
        IWebSocketHandler webSocketHandler,
        IOptions<ApiSettings> settingsOptions) : IForexService
    {
        private readonly HttpConfigs _httpConfigs = httpOptions.Value;
        private readonly ApiSettings _settingsConfigs = settingsOptions.Value;
        public async Task SubscribeToSymbolsPrice(CancellationToken cancellationToken)
        {
            try
            {
                await webSocketHandler.ConnectAsync(_httpConfigs.HubUrl + ApiConstants.Forex);
                await webSocketHandler.SendMessageAsync(new SubscribeMessage
                {
                    EventData = new()
                    {
                        ThresholdLevel = 5,
                        Tickers = _settingsConfigs.ForexSymbolList
                    },
                    Authorization = _settingsConfigs.ApiKey,
                    EventName = "subscribe"
                }, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while getting pricing messages, {error}", ex.Message);
                throw;
            }
        }
    }
}