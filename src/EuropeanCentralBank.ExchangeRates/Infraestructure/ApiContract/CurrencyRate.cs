namespace EuropeanCentralBank.ExchangeRates
{
    using Newtonsoft.Json;

    internal class CurrencyRate
    {
        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "rate")]
        public double Rate { get; set; }
    }
}