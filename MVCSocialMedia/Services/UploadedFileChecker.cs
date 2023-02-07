using MVCSocialMedia.Models;
using System.IO;

namespace MVCSocialMedia.Services
{
    public class UploadedFileChecker : IUploadedFileChecker
    {
        

        public async Task<bool> CheckFileSizeAsync(IFormFile file)
        {
            var memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);

            if (memoryStream.Length < 2097152)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckFileSizeResult(IFormFile file)
        {
            Task<bool> TaskResult = CheckFileSizeAsync(file);
            bool result = TaskResult.GetAwaiter().GetResult();
            return result;
        }

    }
}
