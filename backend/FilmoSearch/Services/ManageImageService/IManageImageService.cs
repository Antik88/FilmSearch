namespace FilmoSearch.Services.ManageImageService
{
    public interface IManageImageService
    {
        Task<string> UploadFile(IFormFile _IFormFile, int id);

        Task<(Stream, string, string)> GetImage(string FileName);
    }
}
