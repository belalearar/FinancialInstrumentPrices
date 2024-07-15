using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.CodeAnalysis;

namespace FinancialInstrumentPrices.Api.Utilities
{
    [ExcludeFromCodeCoverage]
    public static class HubCallerContextUtilities
    {
        public static async Task<T> AbortConnection<T>(Hub hub, T data, string errorMessage = "Server side forceful logoff", string targetMethod = "LogOff")
        {
            await hub.Clients.Caller.SendAsync(targetMethod, errorMessage);
            hub.Context.Abort();
            return data;
        }
    }
}