using FinancialInstrumentPrices.Api;
using FinancialInstrumentPrices.Common.ChannelArgs;
using FinancialInstrumentPrices.Common.Configs;
using FinancialInstrumentPrices.Api.Listeners;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Channels;
using FinancialInstrumentPrices.Infrastructure.Services;
using FinancialInstrumentPrices.Common.Repository;
using FinancialInstrumentPrices.Common.Services;
using FinancialInstrumentPrices.Infrastructure.Repository;


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
        services.AddSignalR().AddJsonProtocol(config =>
        {
            config.PayloadSerializerOptions.PropertyNameCaseInsensitive = true;
            config.PayloadSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            config.PayloadSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
            config.PayloadSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
    });
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapApiEndpoints();
app.UseRouting();
app.Run();