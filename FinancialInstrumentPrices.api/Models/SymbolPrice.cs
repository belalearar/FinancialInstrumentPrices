using Newtonsoft.Json.Linq;

namespace FinancialInstrumentPrices.api.Models
{
    public class SymbolPrice
    {
        public string Service { get; set; } = null!;
        public string MessageType { get; set; } = null!;
        public object Data { get; set; } = null!;
        public string[] PriceData
        {
            get
            {
                var x = JArray.Parse(Data.ToString()!);
                return x.Select(a => a.ToString()).ToArray();
            }
        }
        public string Symbol
        {
            get
            {
                return PriceData[1];
            }
        }
        public DateTime TickTime
        {
            get
            {
                return DateTime.Parse(PriceData[2]);
            }
        }
        public double LastPrice
        {
            get
            {
                return double.Parse(PriceData[5]);
            }
        }
    }
}
