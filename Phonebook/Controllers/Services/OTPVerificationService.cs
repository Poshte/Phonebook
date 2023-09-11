using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Phonebook.Controllers.Services
{
    public class OTPVerificationService : IOTPVerificationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OTPVerificationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public bool IsOTPVerified(string userOTP)
        {
            var storedOTP = GetStoredOTP();
            var isVerified = (userOTP == storedOTP && storedOTP != null);

            return isVerified;
        }

        private string GetStoredOTP()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var storedOTP = httpContext?.Session.GetString("OTP");

            return storedOTP;
        }
    }
}
