using System;

namespace EuropeanCentralBank.ExchangeRates
{
    public class Currency
    {
        public string Symbol { get; set; }

        public double Factor { get; set; }

        public Currencies? GetEquivalent()
        {
            foreach (var info in Enum.GetValues(typeof(Currencies)))
            {
                if (info is Currencies value && value.ISOCode() == Symbol)
                    return value;
            }

            return null;
        }

        public static Currency Euro => new Currency
        {
            Symbol = Currencies.Euro.ISOCode(),
            Factor = 1
        };
    }
}
