using FinancialInstrumentPrices.Common.Models;

namespace FinancialInstrumentPrices.Infrastructure.Mapper
{
    internal static class PriceMapper
    {
        internal static PriceModel ToPriceModel(this ForexPriceResponse priceResponse)
        {
            return new PriceModel
            {
                LastPrice = priceResponse.MidPrice,
                Symbol = priceResponse.Ticker,
                TickTime = priceResponse.QuoteTimestamp
            };
        }

        internal static PriceModel ToPriceModel(this CryptoPriceResponse priceResponse)
        {
            return new PriceModel
            {
                LastPrice = priceResponse.TopOfBookData.First().LastPrice,
                Symbol = priceResponse.Ticker,
                TickTime = priceResponse.TopOfBookData.First().QuoteTimestamp
            };
        }
    }
}