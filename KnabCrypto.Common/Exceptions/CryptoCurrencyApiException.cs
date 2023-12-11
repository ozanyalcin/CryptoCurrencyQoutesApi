using System;

namespace KnabCrypto.Common.Exceptions
{
    public class CryptoCurrencyApiException : Exception
    {
        public CryptoCurrencyApiException(string errorMessage) : base(errorMessage)
        {
        }
    }
}