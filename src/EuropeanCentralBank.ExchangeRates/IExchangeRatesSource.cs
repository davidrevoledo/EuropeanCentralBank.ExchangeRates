namespace EuropeanCentralBank.ExchangeRates
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IExchangeRatesSource
    {
        Task<IEnumerable<Currency>> GetCurrenciesAsync();
    }
}