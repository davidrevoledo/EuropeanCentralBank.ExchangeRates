namespace EuropeanCentralBank.ExchangeRates
{
    using Newtonsoft.Json;

    internal class CurrencyInformation
    {
        [JsonProperty(PropertyName = "Cube")]
        public Exchange Exchange { get; set; }
    }
}