using System;
using System.Threading.Tasks;
using ApplicationContract.Request.Query;
using ApplicationContract.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly IMediator mediator;

    public TodoController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [HttpGet("healthcheck")]
    [ProducesResponseType(200, Type = typeof(ResponseBase<object>))]
    public async Task<IActionResult> HealthCheck()
    {
        var healtCheckQueryResult = await this.mediator.Send(new HealthCheckQuery());
        return Ok(healtCheckQueryResult);
    }
}