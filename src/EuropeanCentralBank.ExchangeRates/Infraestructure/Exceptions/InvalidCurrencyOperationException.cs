namespace EuropeanCentralBank.ExchangeRates
{
    using System;

    public class InvalidCurrencyOperationException : Exception
    {
        public InvalidCurrencyOperationException()
        {

        }

        public InvalidCurrencyOperationException(string errorMessage)
        : base(errorMessage)
        {

        }

        public static InvalidCurrencyOperationException CurrencyNotFound(Currencies currency)
        {
            return new InvalidCurrencyOperationException($"the currency {currency.ISOCode()} was not returned by the Api");
        }
    }
}
