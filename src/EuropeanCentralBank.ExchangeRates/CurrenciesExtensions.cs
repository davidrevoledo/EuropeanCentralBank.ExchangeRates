namespace EuropeanCentralBank.ExchangeRates
{
    public static class CurrenciesExtensions
    {
        /// <summary>
        ///     Get ISO Code for the selected currency or null
        /// </summary>
        /// <param name="currency">selected currency</param>
        /// <returns>ISO Code (string)</returns>
        public static string ISOCode(this Currencies currency)
        {
            Guard.ArgumentNotNull(nameof(currency), currency);

            var type = currency.GetType();
            var memInfo = type.GetMember(currency.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(ISOCodeAttribute), false);
            var value = attributes.Length > 0 ? (ISOCodeAttribute)attributes[0] : null;

            return value?.Code;
        }
    }
}