namespace CryptoGuard.API.Middlewares;

public class LoggingMiddleware(ILogger<LoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var correlationId = context.TraceIdentifier;
        var requestMethod = context.Request.Method;
        var requestPath = context.Request.Path;
        var requestQuery = context.Request.QueryString;

        logger.LogInformation(
            "Request started: CorrelationId={CorrelationId}, Method={Method}, Path={Path}{Query}",
            correlationId, requestMethod, requestPath, requestQuery);

        context.Request.EnableBuffering();
        var requestBody = await ReadBody(context.Request);
        context.Request.Body.Position = 0;

        if (!string.IsNullOrWhiteSpace(requestBody))
        {
            logger.LogDebug(
                "Request body: CorrelationId={CorrelationId}, Body={Body}",
                correlationId, requestBody);
        }

        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        try
        {
            await next(context);
        }
        finally
        {
            stopwatch.Stop();

            var statusCode = context.Response.StatusCode;
            var responseContent = await ReadBody(context.Response);
            
            logger.LogInformation(
                "Request completed: CorrelationId={CorrelationId}, Method={Method}, Path={Path}, StatusCode={StatusCode}, Duration={Duration}ms",
                correlationId, requestMethod, requestPath, statusCode, stopwatch.ElapsedMilliseconds);

            if (!string.IsNullOrWhiteSpace(responseContent))
            {
                logger.LogDebug(
                    "Response body: CorrelationId={CorrelationId}, Body={Body}",
                    correlationId, responseContent);
            }

            context.Response.Body = originalBodyStream;
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }

    private static async Task<string> ReadBody(HttpRequest request)
    {
        request.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(request.Body, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        request.Body.Seek(0, SeekOrigin.Begin);
        return body;
    }

    private static async Task<string> ReadBody(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(response.Body, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);
        return body;
    }
}

