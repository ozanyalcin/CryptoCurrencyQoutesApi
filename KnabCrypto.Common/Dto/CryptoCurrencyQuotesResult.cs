using System.Collections.Generic;

namespace KnabCrypto.Common.Dto
{
    public class CryptoCurrencyQuotesResult
    {
        public Dictionary<string,decimal> Quotes { get; set; }
    }
}