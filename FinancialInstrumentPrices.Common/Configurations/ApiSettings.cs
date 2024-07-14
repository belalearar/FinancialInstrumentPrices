namespace FinancialInstrumentPrices.Common.Configs
{
    public class ApiSettings
    {
        public static string Identifier = "ApiSettings";
        public string ApiKey { get; set; } = null!;
        public string ForexSymbols { get; set; } = null!;
        public string CryptoSymbols { get; set; } = null!;
        public string[] CryptoSymbolList
        {
            get
            {
                return CryptoSymbols.Split(',');
            }
        }
        public string[] ForexSymbolList
        {
            get
            {
                return ForexSymbols.Split(',');
            }
        }
    }
}
