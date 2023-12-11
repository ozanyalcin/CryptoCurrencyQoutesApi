using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnabCrypto.Common.Clients;
using KnabCrypto.Common.Dto;
using KnabCrypto.Common.Settings;

namespace KnabCrypto.Transactions.Queries.Handlers
{
	public class GetCryptoCurrencyQuotesQueryHandler
	{
		private readonly ICryptoCurrencyApiClient _cryptoCurrencyApiClient;
		private readonly IExchangeRatesApiClient _exchangeRatesApiClient;

		public GetCryptoCurrencyQuotesQueryHandler(
			ICryptoCurrencyApiClient cryptoCurrencyApiClient, 
			IExchangeRatesApiClient exchangeRatesApiClient)
		{
			_cryptoCurrencyApiClient = cryptoCurrencyApiClient;
			_exchangeRatesApiClient = exchangeRatesApiClient;
		}

		public async Task<CryptoCurrencyQuotesResult> Handle(GetCryptoCurrencyQuotesQuery request)
		{
			var baseQuote = await _cryptoCurrencyApiClient.GetQuoteFromSymbol(request.CryptoCurrencyCode, Constants.BaseCurrency);
			var exchanges = await _exchangeRatesApiClient.GetExchangeRates(Constants.BaseCurrency);

			return new CryptoCurrencyQuotesResult
			{
				Quotes = CalculateCurrencies(baseQuote.Price, exchanges.Rates)
			};
		}

		private Dictionary<string,decimal> CalculateCurrencies(decimal quotePrice, Dictionary<string, decimal> exchangesRates)
		{
			var result = new Dictionary<string, decimal>();
			var currenciesToBeConverted = Constants.CurrencyToBeConverted.Split(',');	
			foreach (var currency in currenciesToBeConverted)
			{
				if (!exchangesRates.Any(x => x.Key == currency))
				{
					continue;
				}				
				var exchangeRate = exchangesRates.FirstOrDefault(x => x.Key == currency);
				result.Add(exchangeRate.Key, quotePrice * exchangeRate.Value);
			}
			
			return result;
		}
	}
}
