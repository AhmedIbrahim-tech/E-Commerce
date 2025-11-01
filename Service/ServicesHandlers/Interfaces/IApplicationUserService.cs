namespace Service.ServicesHandlers.Interfaces;

public interface IApplicationUserService
{
    Task<string> AddUserAsync(User user, string password);
}
