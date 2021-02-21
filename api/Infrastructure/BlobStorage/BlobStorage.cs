using Application.Common.Interfaces;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.BlobStorage
{
    public class BlobStorage : IBlobStorage
    {
        private readonly IConfiguration _configuration;
        private readonly BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;

        public BlobStorage(
            IConfiguration configuration
            )
        {
            _configuration = configuration;
            _blobServiceClient = new BlobServiceClient(_configuration.GetConnectionString("AccessKey"));
        }

        public async Task DeleteAsync(string fileName, string containerName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }
        public async Task<Stream> DownloadAsync(string fileName, string containerName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
            Response<BlobDownloadInfo> response = await blobClient.DownloadAsync();
            //response.Value -> Tüm bilgileri getirecektir.
            return response.Value.Content;
        }
        public async Task<List<string>> GetLogAsync(string fileName)
        {
            List<string> logs = new List<string>();
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient("logs");//Log olacağı için ismi sabit.
            AppendBlobClient appendBlobClient = _blobContainerClient.GetAppendBlobClient(fileName); //Üzerine eklenebilir olacağından doayı AppendBlobClient seçildi.
            await appendBlobClient.CreateIfNotExistsAsync();
            Response<BlobDownloadInfo> response = await appendBlobClient.DownloadAsync();
            using StreamReader sr = new StreamReader(response.Value.Content);
            string line = string.Empty;
            while ((line = sr.ReadLine()) != null)
                logs.Add(line);
            return logs;
        }
        public List<string> GetNames(string containerNames)
        {
            List<string> blobNames = new List<string>();
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerNames);
            return _blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
        }
        public async Task SetLogAsync(string text, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient("logs");//Log olacağı için ismi sabit.
            await _blobContainerClient.CreateIfNotExistsAsync();
            AppendBlobClient appendBlobClient = _blobContainerClient.GetAppendBlobClient(fileName);
            await appendBlobClient.CreateIfNotExistsAsync();
            using MemoryStream ms = new MemoryStream();
            using StreamWriter sw = new StreamWriter(ms);
            await sw.WriteLineAsync($"{DateTime.Now} | {text}");
            await sw.FlushAsync();
            ms.Position = 0; //Stream 0. akıştan itibaren yazmaya başlayacak.
            await appendBlobClient.AppendBlockAsync(ms);
        }
        public async Task UploadAsnyc(Stream fileStream, string name, string containerName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            //Container yoksa oluşturacak.
            await _blobContainerClient.CreateIfNotExistsAsync();
            //Artık url üzerinden erişime açıldı.
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            BlobClient blobClient = _blobContainerClient.GetBlobClient(name);
            await blobClient.UploadAsync(fileStream);
        }
    }
}
