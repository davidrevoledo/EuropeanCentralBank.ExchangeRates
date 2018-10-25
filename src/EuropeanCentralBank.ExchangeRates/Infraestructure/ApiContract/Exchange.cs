namespace EuropeanCentralBank.ExchangeRates
{
    using Newtonsoft.Json;

    internal class Exchange
    {
        [JsonProperty(PropertyName = "Cube")]
        public Rates Rates { get; set; }
    }
}