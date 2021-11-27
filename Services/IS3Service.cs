using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheMoonshineCafe.Models;

namespace TheMoonshineCafe.Services
{
    public interface IS3Service
    {
        Task<S3Response> CreateBucketAsync(string bucketName);
        Task UploadFileAsync(IFormFile file, string albumName);
        Task UploadBandImageAsync(IFormFile file);
        Task<List<string>> GetPhotos();
        Task<List<string>> GetAlbums();
        Task DeleteImage(string fileName, int bucketChoice);
        Task DeleteFolder(string folderName, int bucketChoice);
    }
}
