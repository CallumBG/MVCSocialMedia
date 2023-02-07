namespace MVCSocialMedia.Services
{
    public interface IUploadedFileChecker
    {
        public bool CheckFileSizeResult(IFormFile file);

        public Task<bool> CheckFileSizeAsync(IFormFile file);
    }
}
