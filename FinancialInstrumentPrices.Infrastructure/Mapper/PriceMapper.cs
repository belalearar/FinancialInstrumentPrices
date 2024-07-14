using FinancialInstrumentPrices.Common.Models;

namespace FinancialInstrumentPrices.Infrastructure.Mapper
{
    internal static class PriceMapper
    {
        internal static PriceModel ToPriceModel(this PriceResponse priceResponse)
        {
            return new PriceModel
            {
                LastPrice = priceResponse.MidPrice,
                Symbol = priceResponse.Ticker,
                TickTime = priceResponse.QuoteTimestamp
            };
        }
    }
}