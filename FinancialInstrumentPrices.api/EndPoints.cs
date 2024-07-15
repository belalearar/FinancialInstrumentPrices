using FinancialInstrumentPrices.Api.Hubs;
using FinancialInstrumentPrices.Common.Services;

namespace FinancialInstrumentPrices.Api;

public static class EndPoints
{
    public static void MapApiEndpoints(this WebApplication app)
    {
        app.MapGet("/api/symbol-price", SymbolPrice);

        app.MapHub<PriceHub>("/hubs/priceHub");
    }

    private static async Task<IResult> SymbolPrice(string symbol, IPriceService priceService, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(symbol))
        {
            return TypedResults.BadRequest();
        }
        var price = await priceService.GetSymbolPrice(symbol, cancellationToken);
        return price is null ? TypedResults.NotFound("Symbol Not Found") : Results.Ok(price);
    }
}