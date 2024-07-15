using FinancialInstrumentPrices.Common.Configs;
using FinancialInstrumentPrices.Common.Extensions;
using FinancialInstrumentPrices.Common.Models;
using FinancialInstrumentPrices.Common.Repository;
using FinancialInstrumentPrices.Common.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics.SymbolStore;

namespace FinancialInstrumentPrices.Infrastructure.Services
{
    public class PriceService(IForexService forexService,
        ICryptoService cryptoService,
        IOptions<ApiSettings> settingOptions,
        ILogger<PriceService> logger,
        ISymbolRepository symbolRepository) : IPriceService
    {
        private readonly ApiSettings _settings = settingOptions.Value;
        public async Task<PriceModel?> GetSymbolPrice(string symbolCode, CancellationToken cancellationToken)
        {
            PriceModel? priceModel = null;
            //Check Symbol Type
            var isForex = symbolCode.IsForex(_settings);
            if (!isForex.HasValue)
            {
                logger.LogWarning("Symbol Not Configured");
                return priceModel;
            }
            //Fetch Symbol Price From Source
            if (isForex.Value)
            {
                priceModel = await forexService.GetSymbolPrice(symbolCode);
            }
            else
            {
                priceModel = await cryptoService.GetSymbolPrice(symbolCode);
            }
            return priceModel;
        }
    }
}