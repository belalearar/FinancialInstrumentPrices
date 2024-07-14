using FinancialInstrumentPrices.Api.Hubs;
using FinancialInstrumentPrices.Common.Repository;

namespace FinancialInstrumentPrices.Api
{
    public static class EndPoints
    {
        public static void MapApiEndpoints(this WebApplication app)
        {
            app.MapGet("/api/symbol-price", SymbolPrice);

            app.MapHub<PriceHub>("/hubs/priceHub");
        }

        private static IResult SymbolPrice(string symbol, ISymbolRepository symbolRepository)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                return TypedResults.BadRequest();
            }
            var price = symbolRepository.GetSymbolPrice(symbol);
            return Results.Ok(price);
        }
    }
}