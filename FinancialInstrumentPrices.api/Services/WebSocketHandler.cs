using FinancialInstrumentPrices.api.Models;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text;
using FinancialInstrumentPrices.api.ChannelArgs;
using System.Threading.Channels;

namespace FinancialInstrumentPrices.api.Services
{
    public class WebSocketHandler(Channel<PriceChannelArgs> channel) : IWebSocketHandler
    {
        private ClientWebSocket WS;
        private CancellationTokenSource CTS;

        public async Task ConnectAsync(string url)
        {
            if (WS != null)
            {
                if (WS.State == WebSocketState.Open) return;
                else WS.Dispose();
            }
            WS = new ClientWebSocket();
            if (CTS != null) CTS.Dispose();
            CTS = new CancellationTokenSource();
            await WS.ConnectAsync(new Uri(url), CTS.Token);
            await Task.Factory.StartNew(ReceiveLoop, CTS.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private async Task ReceiveLoop()
        {
            var loopToken = CTS.Token;
            MemoryStream? outputStream = null;
            WebSocketReceiveResult? receiveResult = null;
            byte[] buffer = [250];
            try
            {
                while (!loopToken.IsCancellationRequested)
                {
                    outputStream = new MemoryStream(250);
                    do
                    {
                        receiveResult = await WS!.ReceiveAsync(buffer, CTS.Token);
                        if (receiveResult.MessageType != WebSocketMessageType.Close)
                            outputStream.Write(buffer, 0, receiveResult.Count);
                    }
                    while (!receiveResult.EndOfMessage);
                    if (receiveResult.MessageType == WebSocketMessageType.Close) break;
                    outputStream.Position = 0;
                    ResponseReceived(outputStream);
                }
            }
            catch (TaskCanceledException) { }
            finally
            {
                outputStream?.Dispose();
            }
        }

        public async Task DisconnectAsync()
        {
            if (WS is null) return;
            // TODO: requests cleanup code, sub-protocol dependent.
            if (WS.State == WebSocketState.Open)
            {
                CTS!.CancelAfter(TimeSpan.FromSeconds(2));
                await WS.CloseOutputAsync(WebSocketCloseStatus.Empty, "", CancellationToken.None);
                await WS.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
            }
            WS.Dispose();
            WS = null;
            CTS!.Dispose();
            CTS = null;
        }

        public async Task SendMessageAsync<RequestType>(RequestType message, CancellationToken cancellationToken)
        {
            var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            await WS!.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, cancellationToken);
        }

        private async void ResponseReceived(Stream inputStream)
        {
            StreamReader reader = new(inputStream);
            string text = reader.ReadToEnd();
            Console.WriteLine(text);
            var price = JsonSerializer.Deserialize<SymbolPrice>(text, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })!;
            if (price.MessageType == "A")
            {
                await channel.Writer.WriteAsync(new PriceChannelArgs
                {
                    LastPrice = price.LastPrice,
                    Symbol = price.Symbol,
                    TickTime = price.TickTime,
                });
            }
            inputStream.Dispose();
        }

        public void Dispose() => DisconnectAsync().Wait();

    }
}