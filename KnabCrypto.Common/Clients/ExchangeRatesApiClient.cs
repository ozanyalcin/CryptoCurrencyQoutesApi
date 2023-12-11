using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using KnabCrypto.Common.Dto;
using KnabCrypto.Common.Exceptions;
using KnabCrypto.Common.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace KnabCrypto.Common.Clients
{
    public class ExchangeRatesApiClient : IExchangeRatesApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ExchangeRatesApiSettings _exchangeRatesApiSettings;

        public ExchangeRatesApiClient(IHttpClientFactory httpClientFactory,IOptions<ExchangeRatesApiSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _exchangeRatesApiSettings = options.Value;
        }

        public async Task<ExchangeRates> GetExchangeRates(string currency)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, CreateRequestUri(currency));
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(httpRequest);
            if (!response.IsSuccessStatusCode)
            {
                throw new ExchangeRatesApiException("An issue occured while requesting exchange rates");
            }
            var apiResponse = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<ExchangeRates>(apiResponse);
        }

        private string CreateRequestUri(string currency)
        {
            return $"{_exchangeRatesApiSettings.BaseUrl}/latest?access_key={_exchangeRatesApiSettings.SecretKey}&base={currency}";
        }
    }
}