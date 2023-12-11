using System.Collections.Generic;
using System.Threading.Tasks;
using KnabCrypto.Common.Dto;

namespace KnabCrypto.Common.Clients
{
    public interface ICryptoCurrencyApiClient
    {
        public Task<CryptoQuote> GetQuoteFromSymbol(string symbol, string currency);
    }
}