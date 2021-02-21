using Application.Common.Interfaces;
using ApplicationContract.Request.Command.FileCommands;
using ApplicationContract.Response.Command.FileCommands;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationService.Handler.Command.FileCommands
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, UploadFileCommandResult>
    {
        private readonly IBlobStorage _blobStorage;

        public UploadFileCommandHandler(
            IBlobStorage blobStorage
            )
        {
            _blobStorage = blobStorage;
        }

        public async Task<UploadFileCommandResult> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                FileStream fileStream = new FileStream(request.FileName, FileMode.OpenOrCreate);
                await _blobStorage.UploadAsnyc(fileStream, Path.GetFileName(request.FileName), "files");
                return new UploadFileCommandResult() { Success = true };
            }
            catch (Exception ex)
            {
                return new UploadFileCommandResult() { Success = false };
            }

        }
    }
}
