using Application.Common.Interfaces;
using ApplicationContract.Contract;
using ApplicationContract.Request.Query.FileQueries;
using ApplicationContract.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationService.Handler.Query.FileCommands
{
    public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, ResponseBase<DownloadFileDto>>
    {
        private readonly IBlobStorage _blobStorage;

        public DownloadFileQueryHandler(
            IBlobStorage blobStorage
            )
        {
            _blobStorage = blobStorage;
        }

        public async Task<ResponseBase<DownloadFileDto>> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
        {
            var stream = await _blobStorage.DownloadAsync(request.FileName, request.ContainerName);

            return new ResponseBase<DownloadFileDto>
            {
                Data = new DownloadFileDto
                {
                    Name = stream.Name,
                    Uri = stream.Uri
                },
                Success = true
            };
        }
    }
}
