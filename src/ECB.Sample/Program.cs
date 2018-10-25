using EuropeanCentralBank.ExchangeRates;
using System;
using System.Threading.Tasks;

namespace ECB.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync()
                .GetAwaiter()
                .GetResult();

            Console.ReadLine();
        }

        private static async Task MainAsync()
        {
            // try get currencies
            IExchangeRatesSource source = new ExchangeRatesSource();

            var rates = await source.GetCurrenciesAsync();

            foreach (var rate in rates)
            {
                Console.WriteLine($"{rate.Symbol} {rate.Factor}");
            }

            // try calculate
            IExchangeRatesCalculator calculator = new ExchangeRatesCalculator(source);

            // from dollar to euro
            double originalAmount = 40;
            var amountInEuros = await calculator.Calculate(Currencies.USDollar, Currencies.Euro, originalAmount);
            Console.WriteLine($"You spent {originalAmount} US and it is equals to {amountInEuros} in EU");

            // from chf to dollar
            originalAmount = 100;
            var amountInDollars = await calculator.Calculate(Currencies.SwissFranc, Currencies.USDollar, originalAmount);
            Console.WriteLine($"You spent {originalAmount} CHF and it is equals to {amountInDollars} in USD");
        }
    }
}
