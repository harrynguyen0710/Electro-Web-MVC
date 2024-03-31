using Microsoft.AspNetCore.Mvc;
using DACS.Service;

namespace WebDT.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailSender _emailSender;
        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string email)
        {
            string subject = "Welcome";
            string message = "Hello world";


            // Send email
            await _emailSender.SendEmailAsync(email, subject, message);

            // Optionally, return a success message or redirect
            return RedirectToAction("Index", "Home");
        }
    }
}
