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