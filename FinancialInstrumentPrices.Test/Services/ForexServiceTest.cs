using FinancialInstrumentPrices.Common.Configs;
using FinancialInstrumentPrices.Common.Messages;
using FinancialInstrumentPrices.Common.Models;
using FinancialInstrumentPrices.Common.Repository;
using FinancialInstrumentPrices.Common.Services;
using FinancialInstrumentPrices.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace FinancialInstrumentPrices.Test.Services
{
    public class ForexServiceTest
    {
        [Fact]
        public async Task SubscribeToSymbolsPrice_Success()
        {
            //Arrange
            var message = new SubscribeMessage
            {
                EventData = new()
                {
                    ThresholdLevel = 5,
                    Tickers = ["btcusd"]
                },
                Authorization = "key",
                EventName = "subscribe"
            };
            Mock<IWebSocketHandler> websocketHandler = new();
            Mock<ILogger<ForexService>> logger = new();
            Mock<IOptions<ApiSettings>> settingMock = new();
            Mock<IOptions<HttpConfigs>> httpConfigMock = new();
            Mock<IHttpRepository> httpRepositoryMock = new();
            settingMock.SetupGet(a => a.Value).Returns(new ApiSettings
            {
                ApiKey = "key",
                ForexSymbols = "eurusd",
            });
            httpConfigMock.SetupGet(a => a.Value).Returns(new HttpConfigs
            {
                HubUrl = "url",
                RestUrl = "url"
            });
            websocketHandler.Setup(a => a.ConnectAsync(It.IsAny<string>()));
            websocketHandler.Setup(a => a.SendMessageAsync(It.IsAny<SubscribeMessage>(), It.IsAny<CancellationToken>()));

            var service = new ForexService(logger.Object, httpConfigMock.Object, websocketHandler.Object, settingMock.Object, httpRepositoryMock.Object);
            //Act
            await service.SubscribeToSymbolsPrice(CancellationToken.None);
            //Assert
            websocketHandler.Verify(a => a.ConnectAsync(It.IsAny<string>()), Times.Once);
            websocketHandler.Verify(a => a.SendMessageAsync(It.Is<SubscribeMessage>(a => a.EventName == message.EventName && a.Authorization == message.Authorization), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task GetSymbolPrice_Success_ShouldReturnPrice()
        {
            //Arrange
            var priceResponses = new List<ForexPriceResponse>
            { new ForexPriceResponse{
                    AskPrice = 200,
                    Ticker = "eurusd",
                    BidPrice = 156,
                    MidPrice=158,
                    QuoteTimestamp = DateTime.UtcNow
                    }};
            Mock<IWebSocketHandler> websocketHandler = new();
            Mock<ILogger<ForexService>> logger = new();
            Mock<IOptions<ApiSettings>> settingMock = new();
            Mock<IOptions<HttpConfigs>> httpConfigMock = new();
            Mock<IHttpRepository> httpRepositoryMock = new();
            settingMock.SetupGet(a => a.Value).Returns(new ApiSettings
            {
                ApiKey = "key",
                ForexSymbols = "eurusd",
            });
            httpConfigMock.SetupGet(a => a.Value).Returns(new HttpConfigs
            {
                HubUrl = "url",
                RestUrl = "url"
            });
            httpRepositoryMock.Setup(a => a.GetAsync<List<ForexPriceResponse>>(It.IsAny<string>())).ReturnsAsync(priceResponses);
            var service = new ForexService(logger.Object, httpConfigMock.Object, websocketHandler.Object, settingMock.Object, httpRepositoryMock.Object);
            //Act
            await service.GetSymbolPrice("eurusd");
            //Assert
            httpRepositoryMock.Verify(a => a.GetAsync<List<ForexPriceResponse>>(It.IsAny<string>()), Times.Once);
        }
    }
}
