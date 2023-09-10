using Phonebook.Models;
using System.Collections.Generic;

namespace Phonebook.Controllers.Services
{
    public class ContactOrderingService : IContactOrderingService
    {
        public void OrderContacts(List<Contact> contacts)
        {
            for (int i = 0; i < contacts.Count; i++)
            {
                contacts[i].Order = i + 1;
            }
        }
    }
}
