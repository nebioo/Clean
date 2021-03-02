using ApplicationContract.Response.Query.FileQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Request.Query.FileQueries
{
    public class GetFileNamesQuery : IRequest<GetFileNamesQueryResult>
    {
        public string ContainerName { get; set; }
    }
}
