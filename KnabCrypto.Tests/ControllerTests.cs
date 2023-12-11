using System.Threading.Tasks;
using KnabCrypto.Api.Controllers;
using KnabCrypto.Transactions.Queries.Handlers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace KnabCrypto.Tests;

public class ControllerTests
{
    [Test]
    public async Task when_crytoCurrencyCode_is_given_empty_it_should_return_bad_request()
    {
        var controller = new CryptoCurrencyController(new GetCryptoCurrencyQuotesQueryHandler(null,null));
        var response = await controller.Get("");
        Assert.IsInstanceOf<BadRequestResult>(response);
    }
}