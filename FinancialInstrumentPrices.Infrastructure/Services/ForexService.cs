using FinancialInstrumentPrices.Common;
using FinancialInstrumentPrices.Common.Configs;
using FinancialInstrumentPrices.Common.Messages;
using FinancialInstrumentPrices.Common.Models;
using FinancialInstrumentPrices.Common.Repository;
using FinancialInstrumentPrices.Common.Services;
using FinancialInstrumentPrices.Infrastructure.Mapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FinancialInstrumentPrices.Infrastructure.Services
{
    /// <summary>
    ///  this service will handle the subscription for forex prices,
    ///  then every message received from the source will write to seperated channel.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="httpOptions"></param>
    /// <param name="webSocketHandler"></param>
    /// <param name="settingsOptions"></param>
    /// <param name="httpRepository"></param>
    public class ForexService(ILogger<ForexService> logger, IOptions<HttpConfigs> httpOptions,
        IWebSocketHandler webSocketHandler,
        IOptions<ApiSettings> settingsOptions,
        IHttpRepository httpRepository) : IForexService
    {
        private readonly HttpConfigs _httpConfigs = httpOptions.Value;
        private readonly ApiSettings _settingsConfigs = settingsOptions.Value;
        public async Task SubscribeToSymbolsPrice(CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(_settingsConfigs.ApiKey))
            {
                throw new ArgumentNullException("ApiKey Is Missing");
            }
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
                logger.LogError(ex, "Error while getting forex pricing messages, {error}", ex.Message);
                throw;
            }
        }

        public async Task<PriceModel?> GetSymbolPrice(string symbol)
        {
            try
            {
                var result = await httpRepository.GetAsync<List<ForexPriceResponse>>(_httpConfigs.RestUrl + ApiConstants.Forex + "/top?tickers=" + symbol + "&token=" + _settingsConfigs.ApiKey);
                return result.First().ToPriceModel();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error While Getting {symbol} Price. Error {error}", symbol, ex.Message);
                throw;
            }
        }
    }
}