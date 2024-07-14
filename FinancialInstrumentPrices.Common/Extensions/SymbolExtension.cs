using FinancialInstrumentPrices.Common.Configs;

namespace FinancialInstrumentPrices.Common.Extensions
{
    public static class SymbolExtension
    {
        public static bool? IsForex(this string symbol,ApiSettings apiSettings)
        {
            if (apiSettings.ForexSymbolList.Contains(symbol))
            {
                return true;
            }
            else if (apiSettings.CryptoSymbolList.Contains(symbol))
            {
                return false;
            }
            return null;
        }
    }
}