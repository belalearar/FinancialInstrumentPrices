namespace FinancialInstrumentPrices.Common.Repository
{
    public interface IHttpRepository
    {
        Task<T> GetAsync<T>(string url);
    }
}