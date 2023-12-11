using System;

namespace KnabCrypto.Common.Exceptions
{
    public class ExchangeRatesApiException : Exception
    {
        public ExchangeRatesApiException(string errorMessage) : base(errorMessage)
        {
        }
    }
}