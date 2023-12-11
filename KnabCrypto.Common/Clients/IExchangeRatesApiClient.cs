using System.Collections.Generic;
using System.Threading.Tasks;
using KnabCrypto.Common.Dto;

namespace KnabCrypto.Common.Clients
{
    public interface IExchangeRatesApiClient
    {
        public Task<ExchangeRates> GetExchangeRates(string currency);
    }
}