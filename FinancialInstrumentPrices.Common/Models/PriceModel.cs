namespace FinancialInstrumentPrices.Common.Models
{
    public class PriceModel
    {
        public string Symbol { get; set; } = null!;
        public DateTime TickTime { get; set; }
        public double LastPrice { get; set; }
    }
}