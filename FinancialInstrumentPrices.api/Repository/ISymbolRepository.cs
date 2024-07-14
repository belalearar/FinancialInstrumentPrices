﻿
using FinancialInstrumentPrices.api.Models;

namespace FinancialInstrumentPrices.api.Repository
{
    public interface ISymbolRepository
    {
        IEnumerable<PriceModel> GetQuotesBySymbolCodes(List<string> symbols);
        PriceModel? GetSymbolPrice(string symbol);
        void UpdateSymbolPrice(PriceModel symbolPrice);
    }
}