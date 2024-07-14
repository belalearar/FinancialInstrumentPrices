using Microsoft.AspNetCore.SignalR;

namespace FinancialInstrumentPrices.api
{
    internal static class HubCallerContextUtilities
    {
        internal static async Task<T> AbortConnection<T>(Hub hub, T data, string errorMessage = "Server side forceful logoff", string targetMethod = "LogOff")
        {
            await hub.Clients.Caller.SendAsync(targetMethod, errorMessage);
            hub.Context.Abort();
            return data;
        }
    }
}