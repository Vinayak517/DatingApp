using System.Threading.Tasks;
using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {

        public readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account
            (
               config.Value.CloudName,
               config.Value.ApiKey,
               config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var UploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();

                var UploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };

                UploadResult = await _cloudinary.UploadAsync(UploadParams);

            }
            return UploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var DeleteParams = new DeletionParams(publicId);

            var result  = await _cloudinary.DestroyAsync(DeleteParams);

            return result;
        }
    }
}