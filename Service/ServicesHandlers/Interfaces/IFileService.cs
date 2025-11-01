namespace Service.ServicesHandlers.Interfaces;

public interface IFileService
{
    Task<string> UploadImageAsync(string location, IFormFile file);
}
