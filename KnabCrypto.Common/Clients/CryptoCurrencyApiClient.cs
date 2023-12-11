using System.Net.Http;
using System.Threading.Tasks;
using KnabCrypto.Common.Dto;
using KnabCrypto.Common.Exceptions;
using KnabCrypto.Common.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace KnabCrypto.Common.Clients
{
    public class CryptoCurrencyApiClient : ICryptoCurrencyApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CryptoCurrencyApiSettings _cryptoCurrencyApiSettings;

        public CryptoCurrencyApiClient(IHttpClientFactory httpClientFactory,IOptions<CryptoCurrencyApiSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _cryptoCurrencyApiSettings = options.Value;
        }

        public async Task<CryptoQuote> GetQuoteFromSymbol(string symbol, string currency)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, CreateRequestUri(symbol, currency));
            httpRequest.Headers.Add("X-CMC_PRO_API_KEY", _cryptoCurrencyApiSettings.SecretKey);
            httpRequest.Headers.Add("Accepts", "application/json");
            
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(httpRequest);
            var apiResponse = response.Content.ReadAsStringAsync().Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new CryptoCurrencyApiException("An issue occured while requesting price of crypto currency");
            }
            
            return new CryptoQuote
            {
                Price = ExtractPriceFromPayload(symbol, currency, apiResponse)
            };
        }

        private static dynamic ExtractPriceFromPayload(string symbol, string currency, string apiResponse)
        {
            var payload = JsonConvert.DeserializeObject<dynamic>(apiResponse);
            var price = payload["data"][symbol.ToUpper()]["quote"][currency.ToUpper()]["price"];
            return price;
        }

        private string CreateRequestUri(string symbol, string currency)
        {
            return $"{_cryptoCurrencyApiSettings.BaseUrl}/cryptocurrency/quotes/latest?symbol={symbol}&convert={currency}";
        }
    }
}