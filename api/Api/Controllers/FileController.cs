using ApplicationContract.Request.Command.FileCommands;
using ApplicationContract.Response.Command.FileCommands;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController : ApiController
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("upload")]
        [ProducesResponseType(200, Type = typeof(UploadFileCommandResult))]
        public async Task<IActionResult> Upload(UploadFileCommand request)
        {
            var uploadFileResult = await _mediator.Send(request);
            return Ok(uploadFileResult);
        }
    }
}
