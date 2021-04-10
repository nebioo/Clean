using ApplicationContract.Contract;
using ApplicationContract.Response;
using MediatR;

namespace ApplicationContract.Request.Command.FileCommands
{
    public class UploadFileCommand : IRequest<ResponseBase<UploadFileDto>>
    {
        public string FileName { get; set; }
    }
}
