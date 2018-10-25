namespace EuropeanCentralBank.ExchangeRates
{
    using System;

    public static class Guard
    {
        public static void ArgumentNotNull(string argumentName, object value)
        {
            if (value == null) throw new ArgumentNullException(argumentName);
        }
    }
}
