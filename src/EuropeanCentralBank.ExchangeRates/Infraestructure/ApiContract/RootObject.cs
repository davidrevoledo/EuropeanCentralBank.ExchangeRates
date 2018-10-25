namespace EuropeanCentralBank.ExchangeRates
{
    using Newtonsoft.Json;

    internal class RootObject
    {
        [JsonProperty(PropertyName = "gesmes:Envelope")]
        public CurrencyInformation Envelope { get; set; }
    }
}