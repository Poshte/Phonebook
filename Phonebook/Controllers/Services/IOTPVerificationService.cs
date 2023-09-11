namespace Phonebook.Controllers.Services
{
    public interface IOTPVerificationService
    {
        bool IsOTPVerified(string userOTP);
    }
}
