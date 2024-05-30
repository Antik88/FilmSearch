using FilmoSearch.Helpers;
using Microsoft.AspNetCore.StaticFiles;

namespace FilmoSearch.Services.ManageImageService
{
    public class ManageImageService : IManageImageService
    {
        public async Task<string> UploadFile(IFormFile _IFormFile, int id)
        {
            string fileName = $"{id}";

            FileInfo _FileInfo = new(_IFormFile.FileName);
            fileName += "_" + _IFormFile.FileName;

            var _GetFilePath = ImageHelper.GetFilePath(fileName);

            using (var _FileStream = new FileStream(_GetFilePath, FileMode.Create))
            {
                await _IFormFile.CopyToAsync(_FileStream);
            }

            return fileName;
        }
        public async Task<(Stream, string, string)> GetImage(string FileName)
        {
            var filePath = ImageHelper.GetFilePath(FileName);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            using var fileStream = new FileStream(filePath, FileMode.Open,
                FileAccess.Read);
            var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            return (memoryStream, contentType, Path.GetFileName(filePath));
        }
    }
}
