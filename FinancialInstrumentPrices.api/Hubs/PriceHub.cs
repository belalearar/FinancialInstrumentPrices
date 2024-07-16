﻿using FinancialInstrumentPrices.Api.Utilities;
using FinancialInstrumentPrices.Common.Models;
using FinancialInstrumentPrices.Common.Repository;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.CodeAnalysis;

namespace FinancialInstrumentPrices.Api.Hubs
{
    [ExcludeFromCodeCoverage]
    public class PriceHub(ISymbolRepository symbolRepository, ILogger<PriceHub> logger) : Hub
    {
        public async Task<IEnumerable<PriceModel>> SubscribeToQuotes(QuoteRequest request)
        {
            if (Context.User == null)
            {
                return await HubCallerContextUtilities.AbortConnection(this, new List<PriceModel>(), "invalid user", "SubscribeToQuotes");
            }

            if (request.Symbols.Count == 0 || request.Symbols.Any(string.IsNullOrEmpty))
            {
                return await HubCallerContextUtilities.AbortConnection(this, new List<PriceModel>(), "Invalid request message", "SubscribeToQuotes");
            }

            var results = symbolRepository.GetQuotesBySymbolCodes(request.Symbols);

            foreach (var symbol in request.Symbols)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, symbol);
            }

            logger.LogInformation("ProductHub::Subscription Request:: ConnectionId: " +
                "{connectionId}, SymbolList: {symbols}", Context.ConnectionId, string.Join(",", request.Symbols));
            return results;
        }
    }
}