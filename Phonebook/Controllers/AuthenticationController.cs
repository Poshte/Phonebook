using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Phonebook.Controllers.Services;
using System;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace Phonebook.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IOTPVerificationService _oTPVerificationService;

        public AuthenticationController(IOTPVerificationService oTPVerificationService)
        {
            _oTPVerificationService = oTPVerificationService;
        }
        public IActionResult Index()
        {           
            var otp = GenerateOTP();
            HttpContext.Session.SetString("OTP", otp);

            var email = User.Identity.Name;
            SendOTPViaEmail(email, otp);

            return View();
        }

        public string GenerateOTP()
        {
            var rnd = new Random();
            return rnd.Next(111111, 999999).ToString();
        }

        public void SendOTPViaEmail(string email, string otp)
        {
            using var client = new MailKit.Net.Smtp.SmtpClient();

            var myMail = "alireza.mohammadi7878@gmail.com";
            client.Connect("smtp.gmail.com", 587);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(myMail, "lqfu xndi ypvn iitt");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Phonebook Web App", myMail));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Phonebook Code Verification";
            message.Body = new TextPart("plain")
            {
                Text = $"Your verification code is: {otp}"
            };

            client.Send(message);
            client.Disconnect(true);
        }
    }
}
