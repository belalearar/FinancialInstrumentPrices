using FinancialInstrumentPrices.api.Hubs;
using FinancialInstrumentPrices.api.Repository;

namespace FinancialInstrumentPrices.api
{
    public static class EndPoints
    {
        public static void MapApiEndpoints(this WebApplication app)
        {
            app.MapGet("/api/symbol-price", SymbolPrice);

            app.MapHub<PriceHub>("/hubs/priceHub");
        }

        private static IResult SymbolPrice(string symbol, HttpContext context, ISymbolRepository symbolRepository)
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