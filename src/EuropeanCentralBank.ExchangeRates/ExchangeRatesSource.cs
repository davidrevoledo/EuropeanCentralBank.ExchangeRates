namespace EuropeanCentralBank.ExchangeRates
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Newtonsoft.Json;

    public class ExchangeRatesSource : IExchangeRatesSource
    {
        private const string Url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        private RootObject _root;

        public async Task<IEnumerable<Currency>> GetCurrenciesAsync()
        {
            await FetchRates()
                .ConfigureAwait(false);

            var currencies = _root.Envelope.Exchange.Rates.CurrencyRates.Select(r => new Currency
            {
                Factor = r.Rate,
                Symbol = r.Currency
            })
            .ToList();

            return currencies;
        }

        private async Task FetchRates()
        {
            try
            {
                await _semaphore.WaitAsync()
                    .ConfigureAwait(false);

                var client = new WebClient();
                var xml = client.DownloadString(Url);

                var doc = new XmlDocument();
                doc.LoadXml(xml);
                var json = JsonConvert.SerializeXmlNode(doc)
                    .Replace("@", string.Empty);

                _root = JsonConvert.DeserializeObject<RootObject>(json);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}