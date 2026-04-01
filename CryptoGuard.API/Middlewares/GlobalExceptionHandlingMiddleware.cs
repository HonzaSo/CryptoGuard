namespace CryptoGuard.API.Middlewares;

public class GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            
            var errorResponse = new
            {
                Message = "An unexpected error occurred.",
                Details = ex.Message
            };
            
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}