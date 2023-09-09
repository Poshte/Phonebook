using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phonebook.DAL;
using Phonebook.Models;
using System.Linq;

namespace Phonebook.Controllers
{
    [Authorize]
    public class PhonebookController : Controller
    {
        private readonly PhonebookContext _dbContext;

        public PhonebookController(PhonebookContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var contacts = _dbContext.Contacts.ToList();
            return View(contacts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Contacts.Add(contact);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(nameof(Create), contact);
        }

        public IActionResult Edit(int id)
        {
            var contact = _dbContext.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        [HttpPost]
        public IActionResult Update(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Contacts.Update(contact);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(nameof(Edit), contact);
        }

        public IActionResult Delete(int id)
        {
            var contact = _dbContext.Contacts.Find(id);
            if (contact == null)
            {
                NotFound();
            }

            return View(contact);
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var contact = _dbContext.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            _dbContext.Contacts.Remove(contact);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
