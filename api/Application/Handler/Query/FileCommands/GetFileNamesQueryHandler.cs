using Application.Common.Interfaces;
using ApplicationContract.Request.Query.FileQueries;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationContract.Contract;
using ApplicationContract.Response;

namespace ApplicationService.Handler.Query.FileCommands
{
    public class GetFileNamesQueryHandler : IRequestHandler<GetFileNamesQuery, ResponseBase<List<FileNameDto>>>
    {
        private readonly IBlobStorage _blobStorage;

        public GetFileNamesQueryHandler(
            IBlobStorage blobStorage
            )
        {
            _blobStorage = blobStorage;
        }

        public async Task<ResponseBase<List<FileNameDto>>> Handle(GetFileNamesQuery request, CancellationToken cancellationToken)
        {
            var files = _blobStorage.GetNames(request.ContainerName);

            return new ResponseBase<List<FileNameDto>>
            {
                Data = files.Select(c => new FileNameDto
                {
                    FileName = c.FileName
                }).ToList(),
                Success = true
            };

        }
    }
}
