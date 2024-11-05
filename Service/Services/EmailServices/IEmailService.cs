namespace Service.Services.EmailServices
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string Email, string Subject, string Html);
    }
}
