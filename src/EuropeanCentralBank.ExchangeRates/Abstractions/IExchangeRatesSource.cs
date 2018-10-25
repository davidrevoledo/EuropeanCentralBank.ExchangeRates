namespace EuropeanCentralBank.ExchangeRates
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IExchangeRatesSource
    {
        /// <summary>
        ///     Get all supported currencies from the European Central Bank
        ///     Euro is not being returned as it will always be 1
        /// </summary>
        /// <returns>A collection of Currencies</returns>
        /// <see cref="Currencies"/>
        Task<IEnumerable<Currency>> GetCurrenciesAsync();
    }
}