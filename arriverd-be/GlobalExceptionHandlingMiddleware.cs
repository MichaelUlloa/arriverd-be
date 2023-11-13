using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace arriverd_be;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected internal server error happened.");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var environment = context.RequestServices.GetRequiredService<IWebHostEnvironment>();

            var problem = new ProblemDetails()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = ex.Message,
            };

            context.Response.ContentType = "application/json";

            string json = JsonSerializer.Serialize(problem);
            await context.Response.WriteAsync(json);
        }
    }
}