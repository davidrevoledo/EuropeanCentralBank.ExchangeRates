//MIT License
//Copyright(c) 2017 David Revoledo

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
// Project Lead - David Revoledo davidrevoledo@d-genix.com

using System;

namespace EuropeanCentralBank.ExchangeRates
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Xml;
    using Newtonsoft.Json;

    public class ExchangeRatesSource : IExchangeRatesSource
    {
        private const string Url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
        private readonly Lazy<WebClient> _webClient = new Lazy<WebClient>(() => new WebClient());

        /// <summary>
        ///     Get all supported currencies from the European Central Bank
        ///     Euro is not being returned as it will always be 1
        /// </summary>
        /// <returns>A collection of Currencies</returns>
        /// <see cref="Currencies"/>
        public async Task<IEnumerable<Currency>> GetCurrenciesAsync()
        {
            var xml = await _webClient.Value.DownloadStringTaskAsync(Url)
                .ConfigureAwait(false);

            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var json = JsonConvert.SerializeXmlNode(doc)
                .Replace("@", string.Empty);

            var root = JsonConvert.DeserializeObject<RootObject>(json);
            var currencies = root.Envelope.Exchange.Rates.CurrencyRates.Select(r => new Currency
            {
                Factor = r.Rate,
                Symbol = r.Currency
            });

            return currencies;
        }
    }
}