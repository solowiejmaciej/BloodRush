#region

#endregion

#region

using BloodRush.API.Exceptions;
using BloodRush.API.Exceptions.ConfirmationCodes;
using FluentValidation;

#endregion

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
        catch (DonorIsRestingException restingException)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(restingException.Message);
        }
        catch (ValidationException validationException)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(validationException.Message);
        }        
        catch (EmailAlreadyConfirmedException emailAlreadyConfirmedException)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(emailAlreadyConfirmedException.Message);
        }
        catch (InvalidCodeException invalidCodeException)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(invalidCodeException.Message);
        }
        catch (PhoneNumberAlreadyConfirmedException phoneNumberAlreadyConfirmedException)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(phoneNumberAlreadyConfirmedException.Message);
        }        
        catch (EmailAlreadyExistsException numberAlreadyConfirmedException)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(numberAlreadyConfirmedException.Message);
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