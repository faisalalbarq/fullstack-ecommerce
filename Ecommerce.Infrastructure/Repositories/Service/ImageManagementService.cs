using Ecommerce.Service.ImageService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Ecommerce.Infrastructure.Repositories.Service
{
    public class ImageManagementService  : IImageManagementService
    {
        private readonly IFileProvider? _fileProvider;

        public ImageManagementService(IFileProvider? fileProvider)
        {
            _fileProvider = fileProvider;
        }


        public async Task<List<string>> AddImageAsync(IFormFileCollection formFiles, string source)
        {
            List<string> saveImageSource = new List<string>();

            var imageDirectory = Path.Combine("wwwroot", "Images", source);

            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }
            
            foreach (var item in formFiles)
            {
                if (item.Length > 0)
                {
                    // get image name
                    var imageName = item.FileName;

                    var imageSource = $"/Images/{source}/{imageName}";

                    var filePath = Path.Combine(imageDirectory, imageName);

                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await item.CopyToAsync(fileStream);
                    }
                    saveImageSource.Add(imageSource);
                }
            }
            return saveImageSource;
        }

        public void DeleteImageAsync(string source)
        {
            var info = _fileProvider?.GetFileInfo(source);

            //if (info == null || !info.Exists || info.IsDirectory)
            //    return Task.CompletedTask;

            var root = info.PhysicalPath;

            File.Delete(root);

            //return Task.CompletedTask;
        }
    }
}
