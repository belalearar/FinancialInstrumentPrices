
namespace FinancialInstrumentPrices.api.ChannelArgs
{
    public class PriceChannelArgs
    {
        public double LastPrice { get; internal set; }
        public string Symbol { get; internal set; } = null!;
        public DateTime TickTime { get; internal set; }
    }
}