using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using KnabCrypto.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace KnabCrypto.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;

        var errorResponse = new ErrorResponse
        {
            Success = false
        };
        switch (exception)
        {
            case CryptoCurrencyApiException ex:
                response.StatusCode = (int) HttpStatusCode.BadRequest;
                errorResponse.Message = ex.Message;
                break;
            case ExchangeRatesApiException ex:
                response.StatusCode = (int) HttpStatusCode.BadRequest;
                errorResponse.Message = ex.Message;
                break;
            default:
                response.StatusCode = (int) HttpStatusCode.InternalServerError;
                errorResponse.Message = "Internal server error!";
                break;
        }
        var result = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(result);
    }
}

public class ErrorResponse
{
    public string Message { get; set; }
    public bool Success { get; set; }

}