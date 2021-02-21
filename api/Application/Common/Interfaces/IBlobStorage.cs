using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IBlobStorage
    {
        Task UploadAsnyc(Stream fileStream, string name, string containerName);
        Task<Stream> DownloadAsync(string fileName, string containerName);
        Task DeleteAsync(string fileName, string containerName);
        Task SetLogAsync(string text, string fileName);
        Task<List<string>> GetLogAsync(string fileName);
        List<string> GetNames(string containerNames);
    }
}
