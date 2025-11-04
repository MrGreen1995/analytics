using Serilog.Context;

namespace UzEx.Analytics.Api.Extensions;

public class RequestContextLoggingMiddleware(RequestDelegate next)
{
    private const string CorrelationIdHeader = "X-Correlation-Id";
    
    private static string GetCorrelationId(HttpContext httpContext)
    {
        httpContext.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId);
    
        return correlationId.FirstOrDefault() ?? httpContext.TraceIdentifier;
    }
    
    public Task InvokeAsync(HttpContext httpContext)
    {
        using (LogContext.PushProperty("CorrelationId", GetCorrelationId(httpContext)))
        {
            return next.Invoke(httpContext);
        }
    }
}