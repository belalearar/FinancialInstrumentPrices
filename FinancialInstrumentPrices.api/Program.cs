using FinancialInstrumentPrices.api;
using FinancialInstrumentPrices.api.ChannelArgs;
using FinancialInstrumentPrices.api.Configs;
using FinancialInstrumentPrices.api.Listeners;
using FinancialInstrumentPrices.api.Repository;
using FinancialInstrumentPrices.api.Services;
using System.Threading.Channels;


var builder = WebApplication.CreateBuilder(args);

builder.Host
    .ConfigureServices((ctx, services) =>
    {
        services.AddHostedService<SubscribeToForexPrices>();
        services.AddHostedService<SubscribeToCryptoPrices>();
        services.AddHostedService<PriceChannelListener>();
        services.AddSingleton<ISymbolRepository, SymbolRepository>();
        services.AddSingleton<ICryptoService, CryptoService>();
        services.AddSingleton<IForexService, ForexService>();
        services.AddTransient<IWebSocketHandler, WebSocketHandler>();
        services.Configure<HttpConfigs>(ctx.Configuration.GetSection(HttpConfigs.Identifier));
        services.Configure<ApiSettings>(ctx.Configuration.GetSection(ApiSettings.Identifier));
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSingleton(Channel.CreateUnbounded<PriceChannelArgs>(new UnboundedChannelOptions() { SingleReader = false }));
    });
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseHttpsRedirection();
app.MapApiEndpoints();
app.Run();