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
namespace EuropeanCentralBank.ExchangeRates
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;

    public class ExchangeRatesCalculator : IExchangeRatesCalculator
    {
        private const int DefaultRoudingPlaces = 2;
        private readonly ConcurrentDictionary<Currencies, Currency> _conversions =
            new ConcurrentDictionary<Currencies, Currency>();

        private readonly IExchangeRatesSource _rateSource;

        public ExchangeRatesCalculator(IExchangeRatesSource rateSource)
        {
            _rateSource = rateSource;
        }

        public Task<double> Calculate(Currencies from, Currencies to, double amount)
        {
            return Calculate(from, to, amount, DefaultRouding);
        }

        public async Task<double> Calculate(Currencies from, Currencies to, double amount, Func<double, double> roundingStrategy)
        {
            Guard.ArgumentNotNull(nameof(roundingStrategy), roundingStrategy);

            if (from == to)
            {
                throw new InvalidCurrencyOperationException($"From and To Currency should be different {from} - {to}");
            }

            if (_conversions.IsEmpty)
            {
                await FetchSimulations()
                    .ConfigureAwait(false);
            }

            double convertedValue;

            // execute calculation
            if (from == Currencies.Euro)
            {
                // euro is straight forward
                var targetRate = _conversions[to];
                if (targetRate == null)
                {
                    throw InvalidCurrencyOperationException.CurrencyNotFound(to);
                }

                convertedValue = targetRate.Factor * amount;
            }
            else
            {
                var sourceRate = _conversions[from];
                if (sourceRate == null)
                {
                    throw InvalidCurrencyOperationException.CurrencyNotFound(from);
                }

                var amountInEuros = amount / sourceRate.Factor;

                var targetRate = to != Currencies.Euro
                    ? _conversions[to]
                    : Currency.Euro;

                if (targetRate == null)
                {
                    throw InvalidCurrencyOperationException.CurrencyNotFound(to);
                }

                convertedValue = amountInEuros * targetRate.Factor;
            }

            return roundingStrategy.Invoke(convertedValue);
        }

        private async Task FetchSimulations()
        {
            var currencies = await _rateSource.GetCurrenciesAsync()
                .ConfigureAwait(false);

            // if the base currency is euro then just save all the factors
            foreach (var currency in currencies)
            {
                var isoEquivalence = currency.GetEquivalent();
                if (!isoEquivalence.HasValue)
                    continue;

                _conversions.AddOrUpdate(isoEquivalence.Value, currency, (old, @new) => @new);
            }
        }

        private static double DefaultRouding(double value)
        {
            var multiplier = Math.Pow(10, DefaultRoudingPlaces);
            return Math.Round(value * multiplier) / multiplier;
        }
    }
}