using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Controllers.Services;
using Phonebook.DAL;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    [Authorize]
    public class PhonebookController : Controller
    {
        private readonly IOTPVerificationService _oTPVerificationService;
        private readonly IContactRepository _contactRepository;
        private readonly IContactOrderingService _orderingService;

        public PhonebookController(IOTPVerificationService oTPVerificationService, IContactRepository contactRepository, IContactOrderingService orderingService)
        {
            _oTPVerificationService = oTPVerificationService;
            _contactRepository = contactRepository;
            _orderingService = orderingService;
        }

        public IActionResult Index(string userOTP)
        {
            var isVerified = HttpContext.Session.GetString("IsVerified");

            if (string.IsNullOrEmpty(isVerified))
            {
                var isOTPVerified = _oTPVerificationService.IsOTPVerified(userOTP);
                if (!isOTPVerified)
                {
                    return RedirectToAction(nameof(Index), "Authentication");
                }

                HttpContext.Session.SetString("IsVerified", "true");
                isVerified = "true";
            }

            var contacts = _contactRepository.GetAllContacts();
            _orderingService.OrderContacts(contacts);

            return View(contacts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _contactRepository.AddContact(contact);
                return RedirectToAction(nameof(Index));
            }

            return View(nameof(Create), contact);
        }

        public IActionResult Edit(int id)
        {
            var contact = _contactRepository.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _contactRepository.UpdateContact(contact);
                return RedirectToAction(nameof(Index), "Phonebook");
            }

            return View(nameof(Edit), contact);
        }

        public IActionResult Delete(int id)
        {
            var contact = _contactRepository.GetContactById(id);
            if (contact == null)
            {
                NotFound();
            }
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            var contact = _contactRepository.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }
            _contactRepository.DeleteContact(contact);

            return RedirectToAction(nameof(Index));
        }
    }
}
