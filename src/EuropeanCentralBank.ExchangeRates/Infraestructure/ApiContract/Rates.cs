namespace EuropeanCentralBank.ExchangeRates
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    internal class Rates
    {
        [JsonProperty(PropertyName = "time")]
        public string Time { get; set; }

        [JsonProperty(PropertyName = "Cube")]
        public List<CurrencyRate> CurrencyRates { get; set; }
    }
}