using Application.Common.Interfaces;
using ApplicationContract.Request.Query.FileQueries;
using ApplicationContract.Response.Query.FileQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationService.Handler.Query.FileCommands
{
    public class GetFileNamesQueryHandler : IRequestHandler<GetFileNamesQuery, GetFileNamesQueryResult>
    {
        private readonly IBlobStorage _blobStorage;

        public GetFileNamesQueryHandler(
            IBlobStorage blobStorage
            )
        {
            _blobStorage = blobStorage;
        }

        public async Task<GetFileNamesQueryResult> Handle(GetFileNamesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var files = _blobStorage.GetNames(request.ContainerName);
                return new GetFileNamesQueryResult() { FileNames = files, Success = true };
            }
            catch (Exception)
            {
                return new GetFileNamesQueryResult() { Success = false };
            }
        }
    }
}
