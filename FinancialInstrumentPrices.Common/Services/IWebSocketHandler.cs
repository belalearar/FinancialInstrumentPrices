

namespace FinancialInstrumentPrices.Common.Services
{
    public interface IWebSocketHandler
    {
        Task ConnectAsync(string url);
        Task DisconnectAsync();
        Task SendMessageAsync<RequestType>(RequestType message, CancellationToken cancellationToken);
    }
}