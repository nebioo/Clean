using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Response
{
    public class ResponseBase
    {
        public bool Success { get; set; } = true;
        public int MessageCode { get; set; }
        public string Message { get; set; }
        public string UserMessage { get; set; }
    }
}
