using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ApplicationContract.Response;
using MediatR;

namespace Infrastructure.IoC.Decorator
{
    public abstract class DecoratorBase<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
      where TResponse : ResponseBase<TResponse> where TRequest : IRequest<TResponse>
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next);


        protected MethodBase GetHandlerMethodInfo()
        {
            var handler = Bootstrapper.Container.Resolve<IRequestHandler<TRequest, TResponse>>();
            if (handler != null)
            {
                return handler.GetType().GetMethod("Handle");
            }

            return null;
        }
    }
}

