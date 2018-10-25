namespace EuropeanCentralBank.ExchangeRates
{
    using System;

    [AttributeUsage(AttributeTargets.Field)]
    internal class ISOCodeAttribute : Attribute
    {
        public ISOCodeAttribute(string code)
        {
            Code = code;
        }

        public string Code { get; }
    }
}
