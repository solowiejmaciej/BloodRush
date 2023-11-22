#region

#endregion

using BloodRush.API.Exceptions;

namespace BloodRush.API.Middleware;

public class ErrorHandlingMiddleware : IMiddleware

{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (DonorNotFoundException notFoundException)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFoundException.Message);
        }
        catch (UnauthorizedAccessException unauthorizedAccess)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync(unauthorizedAccess.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            context.Response.StatusCode = 500;
            //await context.Response.WriteAsync(e.Message);
            await context.Response.WriteAsync("Unknown exception occured");
        }
    }
}