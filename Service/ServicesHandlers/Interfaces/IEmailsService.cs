namespace Service.ServicesHandlers.Interfaces;

public interface IEmailsService
{
    Task<string> SendEmailAsync(string email, string ReturnUrl, EmailType? emailType, Order? order = null);
}
