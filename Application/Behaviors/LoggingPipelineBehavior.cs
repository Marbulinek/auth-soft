using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors;

public class LoggingPipelineBehavior<TRequest, TResponse>(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting request: {@RequestName}, {@DateTimeUtc}", typeof(TRequest).Name, DateTime.UtcNow);
        
        var result = await next();

        logger.LogInformation("Completed request: {@RequestName}, {@DateTimeUtc}", typeof(TRequest).Name, DateTime.UtcNow);
        
        return result;
    }
}