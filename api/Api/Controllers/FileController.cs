using ApplicationContract.Request.Command.FileCommands;
using ApplicationContract.Request.Query.FileQueries;
using ApplicationContract.Response.Command.FileCommands;
using ApplicationContract.Response.Query.FileQueries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
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

        [HttpPost("download")]
        [ProducesResponseType(200, Type = typeof(DownloadFileQueryResult))]
        public async Task<IActionResult> Download(DownloadFileQuery request)
        {
            var downloadFileResult = await _mediator.Send(request);
            return Ok(downloadFileResult);
        }

        [HttpPost("getFileList")]
        [ProducesResponseType(200, Type = typeof(GetFileNamesQueryResult))]
        public async Task<IActionResult> GetFileNames(GetFileNamesQuery request)
        {
            var getFileResult = await _mediator.Send(request);
            return Ok(getFileResult);
        }

    }
}
