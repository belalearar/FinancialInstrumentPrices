namespace FinancialInstrumentPrices.api.Models
{
    public class SubscribeMessage
    {
        public string EventName { get; set; } = null!;
        public string Authorization { get; set; } = null!;
        public EventData EventData { get; set; } = new();
    }
    public class EventData
    {
        public int ThresholdLevel { get; set; }
        public string[] Tickers { get; set; } = [];
    }
}