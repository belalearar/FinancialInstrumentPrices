using FinancialInstrumentPrices.api.Configs;
using FinancialInstrumentPrices.api.Models;
using Microsoft.Extensions.Options;

namespace FinancialInstrumentPrices.api.Services
{
    public class CryptoService(ILogger<CryptoService> logger, IOptions<HttpConfigs> httpOptions,
        IWebSocketHandler webSocketHandler,
        IOptions<ApiSettings> settingsOptions) : ICryptoService
    {
        private readonly HttpConfigs _httpConfigs = httpOptions.Value;
        private readonly ApiSettings _settingsConfigs = settingsOptions.Value;
        public async Task SubscribeToSymbolsPrice(CancellationToken cancellationToken)
        {
            try
            {
                await webSocketHandler.ConnectAsync(_httpConfigs.HubUrl + ApiConstants.Crypto);
                await webSocketHandler.SendMessageAsync(new SubscribeMessage
                {
                    EventData = new()
                    {
                        ThresholdLevel = 5,
                        Tickers = _settingsConfigs.CryptoSymbolList
                    },
                    Authorization = _settingsConfigs.ApiKey,
                    EventName = "subscribe"
                }, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while getting crypto pricing messages, {error}", ex.Message);
                throw;
            }
        }
    }
}