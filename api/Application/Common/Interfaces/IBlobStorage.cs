using ApplicationService.Common.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ApplicationContract.Contract;

namespace Application.Common.Interfaces
{
    public interface IBlobStorage
    {
        Task UploadAsnyc(Stream fileStream, string name, string containerName);
        Task<BlobDownloadModel> DownloadAsync(string fileName, string containerName);
        Task DeleteAsync(string fileName, string containerName);
        Task SetLogAsync(string text, string fileName);
        Task<List<string>> GetLogAsync(string fileName);
        List<FileNameDto> GetNames(string containerNames);
    }
}
