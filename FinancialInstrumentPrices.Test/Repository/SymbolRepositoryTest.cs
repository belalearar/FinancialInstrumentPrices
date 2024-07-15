using FinancialInstrumentPrices.Infrastructure.Repository;
using Xunit;

namespace FinancialInstrumentPrices.Test.Repository
{
    public class SymbolRepositoryTest
    {
        [Fact]
        public void UpdateSymbolPrice_Success()
        {
            //Arrange

            var service = new SymbolRepository();
            Common.Models.PriceModel symbolPrice = new Common.Models.PriceModel
            {
                LastPrice = 150,
                Symbol = "eurusd",
                TickTime = DateTime.UtcNow,
            };
            service.UpdateSymbolPrice(symbolPrice);
            //Act
            var price = service.GetQuotesBySymbolCodes(["eurusd"]);
            //Assert
            Assert.Equal(price.First().Symbol, symbolPrice.Symbol);
            Assert.Equal(price.First().LastPrice, symbolPrice.LastPrice);
            Assert.Equal(price.First().TickTime, symbolPrice.TickTime);
        }
    }
}
