using System;

namespace KnabCrypto.Transactions.Queries
{
	public class GetCryptoCurrencyQuotesQuery
	{
		public GetCryptoCurrencyQuotesQuery(string cryptoCurrencyCode)
		{
			CryptoCurrencyCode = cryptoCurrencyCode;
			
		}
		public string CryptoCurrencyCode { get; set; }
	}
}
