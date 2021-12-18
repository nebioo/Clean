using Application.Common.Interfaces;
using ApplicationContract.Request.Command.FileCommands;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ApplicationContract.Contract;
using ApplicationContract.Response;

namespace ApplicationService.Handler.Command.FileCommands
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, ResponseBase<UploadFileDto>>
    {
        private readonly IBlobStorage _blobStorage;

        public UploadFileCommandHandler(IBlobStorage blobStorage)
        {
            _blobStorage = blobStorage;
        }

        public async Task<ResponseBase<UploadFileDto>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var fileStream = new FileStream(request.FileName, FileMode.OpenOrCreate);
            await _blobStorage.UploadAsnyc(fileStream, Path.GetFileName(request.FileName), "files");

            return new ResponseBase<UploadFileDto>
            {
                Success = true
            };

        }
    }
}
