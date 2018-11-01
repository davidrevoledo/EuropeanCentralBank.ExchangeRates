//MIT License
//Copyright(c) 2017 David Revoledo

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
// Project Lead - David Revoledo davidrevoledo@d-genix.com
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
