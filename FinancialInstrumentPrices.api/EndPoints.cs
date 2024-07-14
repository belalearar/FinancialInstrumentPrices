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

        private static async Task<IResult> SymbolPrice(string symbol, ISymbolRepository symbolRepository, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                return TypedResults.BadRequest();
            }
            var price = await symbolRepository.GetSymbolPrice(symbol, cancellationToken);
            return price is null ? TypedResults.NotFound() : Results.Ok(price);
        }
    }
}