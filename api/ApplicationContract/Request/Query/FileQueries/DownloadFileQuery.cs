using ApplicationContract.Response;
using MediatR;
using ApplicationContract.Contract;

namespace ApplicationContract.Request.Query.FileQueries
{
    public class DownloadFileQuery : IRequest<ResponseBase<DownloadFileDto>>
    {
        public string FileName { get; set; }
        public string ContainerName { get; set; }
    }
}
