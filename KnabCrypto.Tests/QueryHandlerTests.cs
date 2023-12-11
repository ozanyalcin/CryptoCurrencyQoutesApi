using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnabCrypto.Common.Clients;
using KnabCrypto.Common.Dto;
using KnabCrypto.Common.Settings;
using KnabCrypto.Transactions.Queries;
using KnabCrypto.Transactions.Queries.Handlers;
using Moq;
using NUnit.Framework;

namespace KnabCrypto.Tests
{
    
    public class QueryHandlerTests
    {
        [Test]
        public async Task when_crypto_currency_quotes_requested_with_code_handler_should_calculate_correct_values()
        {
            var cryptoCode = "BTC";
            
            var mockCryptoCurrencyClient = ArrangeMockCryptoCurrencyClient(cryptoCode, Constants.BaseCurrency);
            var mockExchangeRatesApiClient = ArrangeMockExchangeRatesApiClient(Constants.BaseCurrency);

            var queryHandler =
                new GetCryptoCurrencyQuotesQueryHandler(
                    mockCryptoCurrencyClient.Object,
                    mockExchangeRatesApiClient.Object);

            var result = await queryHandler.Handle(new GetCryptoCurrencyQuotesQuery(cryptoCode));
            Assert.True(result.Quotes.Any(x=> x is { Key: "EUR", Value: (decimal)32 }));
            Assert.True(result.Quotes.Any(x=> x is { Key: "USD", Value: (decimal)49.92}));
            Assert.True(result.Quotes.Any(x=> x is { Key: "AUD", Value: (decimal)68.224 }));        
            Assert.True(result.Quotes.Any(x=> x is { Key: "GBP", Value: (decimal)99.9392}));      
        }



        [Test]
        public async Task when_crypto_currency_quotes_handler_called_crytoCurrencyApi_and_exhangeRatesApi_called_with_correct_values()
        {
            var cryptoCode = "ETH";
            
            var mockCryptoCurrencyClient = ArrangeMockCryptoCurrencyClient(cryptoCode, Constants.BaseCurrency);
            var mockExchangeRatesApiClient = ArrangeMockExchangeRatesApiClient(Constants.BaseCurrency);
            
            var queryHandler =
                new GetCryptoCurrencyQuotesQueryHandler(
                    mockCryptoCurrencyClient.Object,
                    mockExchangeRatesApiClient.Object);
            
            await queryHandler.Handle(new GetCryptoCurrencyQuotesQuery("ETH"));

            mockCryptoCurrencyClient.Verify(x => x.GetQuoteFromSymbol(cryptoCode, Constants.BaseCurrency), Times.Once);
            mockExchangeRatesApiClient.Verify(x => x.GetExchangeRates(Constants.BaseCurrency), Times.Once);
        }
        
        private static Mock<IExchangeRatesApiClient> ArrangeMockExchangeRatesApiClient(string baseCurrency)
        {
            var mockExchangeRatesApiClient = new Mock<IExchangeRatesApiClient>();
            mockExchangeRatesApiClient.Setup(x => x.GetExchangeRates(baseCurrency))
                .ReturnsAsync(new ExchangeRates
                {
                    Rates = new Dictionary<string, decimal>
                    {
                        { "EUR", 1 }, { "USD", new decimal(1.56) }, { "AUD", new decimal(2.132) },
                        { "GBP", new decimal(3.1231) }
                    }
                });
            return mockExchangeRatesApiClient;
        }

        private static Mock<ICryptoCurrencyApiClient> ArrangeMockCryptoCurrencyClient(string cryptoCode, string baseCurreny)
        {
            var mockCryptoCurrencyClient = new Mock<ICryptoCurrencyApiClient>();
            mockCryptoCurrencyClient.Setup(x => x.GetQuoteFromSymbol(cryptoCode, baseCurreny))
                .ReturnsAsync(new CryptoQuote
                {
                    Price = 32
                });
            return mockCryptoCurrencyClient;
        }
    }
}