using Application.Common.Interfaces;
using ApplicationContract.Request.Query.FileQueries;
using ApplicationContract.Response.Query.FileQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationService.Handler.Query.FileCommands
{
    public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, DownloadFileQueryResult>
    {
        private readonly IBlobStorage _blobStorage;

        public DownloadFileQueryHandler(
            IBlobStorage blobStorage
            )
        {
            _blobStorage = blobStorage;
        }

        public async Task<DownloadFileQueryResult> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var stream = await _blobStorage.DownloadAsync(request.FileName, request.ContainerName);

                return new DownloadFileQueryResult()
                {
                    Name = stream.Name,
                    Uri = stream.Uri,
                    Success = true
                };
            }
            catch (Exception)
            {
                return new DownloadFileQueryResult() { Success = false };
            }

        }
    }
}
