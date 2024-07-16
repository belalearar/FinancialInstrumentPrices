 FinancialInstrumentPrices

To run this application please do the following:-

* copy the **ApiKey** from the email and add it in appsettings.Json

* navigate to folder interminal ~\FinancialInstrumentPrices\FinancialInstrumentPrices.Api

* run the following command : 
	dotnet run

* run the application and navigate to 
	https://localhost:7031/swagger/index.html 
here you can use the rest api to get last price by symbol.

* To Subscribe to WebSocket use postman WebSocket request, then add this link:
	wss://localhost:7031/hubs/priceHub

* send this this message as it is to start receiving quotes:

{"protocol":"json","version":1}
{"arguments":[{"symbols":["btcusd","eurusd","usdjpy"]}],"invocationId":"0","streamIds":[],"target":"SubscribeToQuotes","type":1}

look into methods summary for code clarification.