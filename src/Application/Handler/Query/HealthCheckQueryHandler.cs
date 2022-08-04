using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicationContract.Request.Query;
using ApplicationContract.Response;
using Domain.Common;
using MediatR;

namespace ApplicationService.Handler.Query;

public class HealthCheckQueryHandler : IRequestHandler<HealthCheckQuery, ResponseBase<object>>
{
    public HealthCheckQueryHandler()
    {

    }

    public Task<ResponseBase<object>> Handle(HealthCheckQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new ResponseBase<object>
        {
            Success = true,
            Message = "I'm alive and well :)",
            MessageCode = ApplicationMessage.Success
        });
    }
}