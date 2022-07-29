using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ApplicationContract.Response;
using ApplicationService.Common;
using MediatR;

namespace Infrastructure.IoC.Decorator;

public class LoggingHandler<TRequest, TResponse> : DecoratorBase<TRequest, TResponse>
    where TRequest : IRequest<TResponse> where TResponse : ResponseBase
{
    private readonly IAppLogger _appLogger;

    public LoggingHandler(IAppLogger appLogger)
    {
        _appLogger = appLogger;
    }

    public override async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var handlerMethodInfo = GetHandlerMethodInfo();

        _appLogger.MethodEntry(request, handlerMethodInfo);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();

        _appLogger.MethodExit(response, handlerMethodInfo, timer.Elapsed.TotalMilliseconds, response.MessageCode);

        return response;
    }
}