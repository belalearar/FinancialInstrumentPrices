namespace FinancialInstrumentPrices.Common.Models
{
    public class PriceResponse
    {
        public string Ticker { get; set; } = null!;
        public DateTime QuoteTimestamp { get; set; }
        public double BidPrice { get; set; }
        public double AskPrice { get; set; }
        public double MidPrice { get; set; }
    }
}