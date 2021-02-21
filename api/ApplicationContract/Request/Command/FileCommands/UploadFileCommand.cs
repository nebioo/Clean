using ApplicationContract.Response.Command.FileCommands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContract.Request.Command.FileCommands
{
    public class UploadFileCommand : IRequest<UploadFileCommandResult>
    {
        public string FileName { get; set; }
    }
}
