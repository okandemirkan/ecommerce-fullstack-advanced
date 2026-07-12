using MediatR;
using Microsoft.Extensions.Logging;
namespace Application.Behaviours
{
    public class ExceptionBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<ExceptionBehavior<TRequest, TResponse>> _logger;
        public ExceptionBehavior(ILogger<ExceptionBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, 
            RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }

            catch(Exception ex) 
            {
                _logger.LogError("Request : {Request Name} could not be completed | Error : {Message}",
                    typeof(TRequest).Name, ex.Message);
                throw;
            }
        }
    }
}
