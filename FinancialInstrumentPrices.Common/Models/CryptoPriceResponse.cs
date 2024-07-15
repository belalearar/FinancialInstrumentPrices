namespace FinancialInstrumentPrices.Common.Models
{
    public class CryptoPriceResponse : PriceResponse
    {
        public string BaseCurrency { get; set; } = null!;
        public string QuoteCurrency { get; set; } = null!;
        public List<CryptoTopOfBookData> TopOfBookData { get; set; } = [];
    }

    public class CryptoTopOfBookData
    {
        public double AskSize { get; set; }
        public double BidSize { get; set; }
        public DateTime LastSaleTimestamp { get; set; }
        public double LastPrice { get; set; }
        public double AskPrice { get; set; }
        public DateTime QuoteTimestamp { get; set; }
        public string BidExchange { get; set; } = string.Empty;
        public double LastSizeNotional { get; set; }
        public string LastExchange { get; set; } = string.Empty;
        public string AskExchange { get; set; } = string.Empty;
        public double BidPrice { get; set; }
        public double LastSize { get; set; }
    }
}