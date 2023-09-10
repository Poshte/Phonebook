using Phonebook.Models;
using System.Collections.Generic;

namespace Phonebook.DAL
{
    public interface IContactRepository
    {
        List<Contact> GetAllContacts();
        Contact GetContactById(int id);
        void AddContact(Contact contact);
        void UpdateContact(Contact contact);
        void DeleteContact(Contact contact);
    }
}
