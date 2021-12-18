using System.Collections.Generic;
using ApplicationContract.Contract;
using ApplicationContract.Response;
using MediatR;

namespace ApplicationContract.Request.Query.FileQueries
{
    public class GetFileNamesQuery : IRequest<ResponseBase<List<FileNameDto>>>
    {
        public string ContainerName { get; set; }
    }
}
