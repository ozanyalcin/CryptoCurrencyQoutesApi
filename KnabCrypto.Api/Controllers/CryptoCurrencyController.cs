using System.Threading.Tasks;
using KnabCrypto.Transactions.Queries;
using KnabCrypto.Transactions.Queries.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace KnabCrypto.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptoCurrencyController : ControllerBase
    {
        private readonly GetCryptoCurrencyQuotesQueryHandler _queryHandler;

        public CryptoCurrencyController(
            GetCryptoCurrencyQuotesQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string cryptoCurrencyCode)
        {
            if (string.IsNullOrWhiteSpace(cryptoCurrencyCode))
            {
                return BadRequest();
            }
            
            var result = 
                await _queryHandler.Handle(new GetCryptoCurrencyQuotesQuery(cryptoCurrencyCode));
            
            return Ok(result);
        }
    }
}