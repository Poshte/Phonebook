using Phonebook.Models;
using System.Collections.Generic;
using System.Linq;

namespace Phonebook.DAL
{
    public class ContactRepository : IContactRepository
    {
        private readonly PhonebookContext _dbContext;

        public  ContactRepository(PhonebookContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Contact> GetAllContacts()
        {
            return _dbContext.Contacts.ToList();
        }

        public Contact GetContactById(int id)
        {
            return _dbContext.Contacts.Find(id);
        }

        public void AddContact(Contact contact)
        {
            contact.CreatedAt = System.DateTime.Now;
            _dbContext.Contacts.Add(contact);
            _dbContext.SaveChanges();
        }

        public void UpdateContact(Contact contact)
        {
            _dbContext.Contacts.Update(contact);
            _dbContext.SaveChanges();
        }
        public void DeleteContact(Contact contact)
        {
            _dbContext.Contacts.Remove(contact);
            _dbContext.SaveChanges();
        }
    }
}