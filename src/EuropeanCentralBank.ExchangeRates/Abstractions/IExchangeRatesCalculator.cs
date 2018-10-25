namespace EuropeanCentralBank.ExchangeRates
{
    using System;
    using System.Threading.Tasks;

    public interface IExchangeRatesCalculator
    {
        Task<double> Calculate(Currencies from, Currencies to, double amount);

        Task<double> Calculate(Currencies from, Currencies to, double amount, Func<double, double> roundingStrategy);
    }
}