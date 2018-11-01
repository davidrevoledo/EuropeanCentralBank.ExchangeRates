# EuropeanCentralBank.ExchangeRates
European CentralBank  ExchangeRates Api Client for C#  / uses the European Central Bank's daily feed for accuracy

Port of https://github.com/facundofarias/ecb-exchange-rates

# Installation

# Usage
Using the libary should be straight forward 

Get Currencies information 
(Interfaces can be mocked for testing purpose)
``` C#
    IExchangeRatesSource source = new ExchangeRatesSource();
    var rates = await source.GetCurrenciesAsync();

```
Currencies conversion (always using Euro as a base)
Using double rule of 3 for calculation with a non euro base conversion.
``` C#
    double originalAmount = 40;
    var amountInEuros = await calculator.Calculate(Currencies.USDollar, Currencies.Euro, originalAmount);
    Console.WriteLine($"You spent {originalAmount} US and it is equals to {amountInEuros} in EU");

    // from chf to dollar
    originalAmount = 100;
    var amountInDollars = await calculator.Calculate(Currencies.SwissFranc, Currencies.USDollar, originalAmount);
    Console.WriteLine($"You spent {originalAmount} CHF and it is equals to {amountInDollars} in USD");
```

Here is a sample  https://github.com/davidrevoledo/EuropeanCentralBank.ExchangeRates/tree/master/src/ECB.Sample

# Supported Currencies

 * AUD - Australian Dollar
 * BGN - Bulgarian Lev
 * BRL - Brazilian Real
 * CAD - Canadian Dollar
 * CHF - Swiss Franc
 * CNY - Chinese Yuan
 * CZK - Czech Koruna
 * DKK - Danish Krone
 * EUR - Euro
 * GBP - British Pound
 * HKD - Hong Kong Dollar
 * HRK - Croatian Kuna
 * HUF - Hungarian Forint
 * IDR - Indonesian Rupiah
 * ILS - Israeli New Shekel
 * INR - Indian Rupee
 * JPY - Japanese Yen
 * KRW - South Korean Won
 * LTL - Lithuanian Litas
 * LVL - Latvian Lats
 * MXN - Mexian Peso
 * MYR - Malaysian Ringgit
 * NOK - Norwegian Krone
 * NZD - New Zealand Dollar
 * PHP - Phillippine Peso
 * PLN - Polish Zloty
 * RON - Romanian New Leu
 * RUB - Russian Rouble
 * SEK - Swedish Krona
 * SGD - Singapore Dollar
 * THB - Thai Baht
 * TRY - Turkish Lira
 * USD - US Dollar
 * ZAR - South African Rand
 
 Disclaimer ECB Api may remove some of the supported currencies, the library is ready to don't break in that case but nothing will be retrieved for that currency, for more information please see : 
https://www.ecb.europa.eu/stats/policy_and_exchange_rates/euro_reference_exchange_rates/html/index.en.html
