using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Response.Query.FileQueries
{
    public class GetFileNamesQueryResult : ResponseBase
    {
        public List<string> FileNames { get; set; }
    }
}
