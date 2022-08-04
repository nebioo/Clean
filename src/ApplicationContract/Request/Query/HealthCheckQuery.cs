using System;
using ApplicationContract.Response;
using MediatR;

namespace ApplicationContract.Request.Query;

public class HealthCheckQuery : IRequest<ResponseBase<object>>
{

}
