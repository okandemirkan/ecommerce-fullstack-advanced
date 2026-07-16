using MediatR;
using Microsoft.Extensions.Logging;
namespace Application.Behaviours
{
    public class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var response = await next();
            var requestName = typeof(TRequest).Name;

            if (response is IEnumerable<object> list)
            {
                _logger.LogInformation("Request: {RequestName} | Response: Successfully returned {Count} records!",
                    typeof(TRequest).Name, list.Count());
            }
            else
            {
                _logger.LogInformation("Request: {RequestName} completed successfully.",requestName);
            }
            return response;
        }
    }
}
