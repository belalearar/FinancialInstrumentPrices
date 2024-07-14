namespace FinancialInstrumentPrices.Common.ChannelArgs
{
    public class PriceChannelArgs
    {
        public double LastPrice { get; set; }
        public string Symbol { get; set; } = null!;
        public DateTime TickTime { get; set; }
    }
}